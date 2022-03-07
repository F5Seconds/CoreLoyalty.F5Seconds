using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Common;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.GotIt.GotItBuyVoucherRes;

namespace CoreLoyalty.F5Seconds.GotIt.Repositories
{
    public class GotItHttpClientRepository : IGotItHttpClientService
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly ILogger<GotItHttpClientRepository> _logger;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private string Partner = "GOTIT";
        public GotItHttpClientRepository(
            HttpClient client, 
            IMapper mapper, 
            ILogger<GotItHttpClientRepository> logger, 
            IBus bus, 
            IConfiguration config, 
            IWebHostEnvironment env)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
            _bus = bus;
            _config = config;
            _env = env;
        }
        public async Task<Application.Wrappers.Response<List<F5sVoucherCode>>> BuyVoucherAsync(GotItBuyVoucherReq voucher)
        {
            string payloadBuyVoucher = JsonConvert.SerializeObject(voucher, Formatting.Indented);
            var transReq = _mapper.Map<GotItTransactionRequest>(voucher, opt => opt.AfterMap((s, d) => {
                d.Channel = voucher.channel;
                d.Partner = Partner; 
                d.Payload = payloadBuyVoucher;
            }));
            var requestEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(1, _env.IsProduction(), _config));
            var resSuccessEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(2, _env.IsProduction(), _config));
            var resFailEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(3, _env.IsProduction(), _config));
            var content = new StringContent(payloadBuyVoucher, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/transaction", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    transReq.Status = 0;
                    await requestEndpoint.Send(transReq);
                    await resFailEndpoint.Send(new GotItTransactionResFail()
                    {
                        Code = error.code,
                        Message = error.msg,
                        Partner = Partner,
                        TransactionId = transReq.TransactionId,
                        ProductCode = transReq.ProductCode,
                        Payload = jsonString,
                        Created = DateTime.Now
                    });
                    return new Application.Wrappers.Response<List<F5sVoucherCode>>(false, null, error.code, new List<string> { error.msg });
                }
                var resultV = JsonConvert.DeserializeObject<List<GotItBuyVoucherRes>>(jsonString);
                List<VoucherInfoRes> vRes = new List<VoucherInfoRes>();
                foreach (var i in resultV)
                {
                    foreach (var v in i.vouchers)
                    {
                        vRes.Add(v);
                    }
                }
                transReq.Status = 1;
                await requestEndpoint.Send(transReq);
                return new Application.Wrappers.Response<List<F5sVoucherCode>>(true,FormatVoucherCode(voucher,vRes, resSuccessEndpoint,jsonString));
            }
            transReq.Status = -1;
            await requestEndpoint.Send(transReq);
            var errorStr = await response.Content.ReadAsStringAsync();
            await resFailEndpoint.Send(new GotItTransactionResFail()
            {
                Code = response.StatusCode.ToString(),
                Message = errorStr,
                Partner = Partner,
                TransactionId = transReq.TransactionId,
                ProductCode = transReq.ProductCode,
                Created = DateTime.Now
            });
            
            return new Application.Wrappers.Response<List<F5sVoucherCode>>(false, null, errorStr);
        }

        private List<F5sVoucherCode> FormatVoucherCode(GotItBuyVoucherReq vReq, List<VoucherInfoRes> vRes, ISendEndpoint endPoint,string payload)
        {
            List<F5sVoucherCode> f5SVoucherCodes = new List<F5sVoucherCode>();
            foreach (var v in vRes)
            {
                f5SVoucherCodes.Add(new F5sVoucherCode()
                {
                    customerPhone = vReq.phone,
                    expiryDate = vReq.expiryDate,
                    transactionId = vReq.voucherRefId,
                    voucherCode = v.voucherCode,
                    propductId = vReq.productCode,
                    productPrice = vReq.productPrice
                });
                bool expried = DateTime.TryParseExact(v.expiryDate, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime expiryDate);
                endPoint.Send(new GotItTransactionResponse()
                {
                    Channel = vReq.channel,
                    Created = DateTime.Now,
                    CustomerPhone = vReq.phone,
                    ExpiryDate = expried ? expiryDate : null,
                    ProductPrice = vReq.productPrice,
                    ProductCode = vReq.productCode,
                    TransactionId = vReq.voucherRefId,
                    VoucherCode = v.voucherCode,
                    Payload = payload,
                    VoucherImageLink = v.voucherImageLink,
                    VoucherLink  = v.voucherLink,
                    VoucherLinkCode = v.voucherLinkCode,
                    Type = v.product.productType,
                    BrandId = v.product.brandId,
                    BrandName = v.product.brandNm
                }).Wait();
            }
            return f5SVoucherCodes;
        }

        public async Task<Application.Wrappers.Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherDetail() { productId = id }), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/detail", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Application.Wrappers.Response<F5sVoucherDetail>(false, null, error.code, new List<string> { error.msg });
                }
                var product = JsonConvert.DeserializeObject<GotItVoucherDetail>(jsonString);
                return new Application.Wrappers.Response<F5sVoucherDetail>(true, _mapper.Map<F5sVoucherDetail>(product));
            }
            return new Application.Wrappers.Response<F5sVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Application.Wrappers.Response<List<F5sVoucherBase>>> VoucherListAsync()
        {
            var content = new StringContent(JsonConvert.SerializeObject(new PayloadGotItVoucherList()), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/product/list", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                if (Helpers.TryParseJsonConvert(jsonString, out GotItErrorMessage error))
                {
                    return new Application.Wrappers.Response<List<F5sVoucherBase>>(false, null, error.code, new List<string> { error.msg });
                }
                var listV = new List<F5sVoucherBase>();
                var res = JsonConvert.DeserializeObject<GotItVoucherList>(jsonString);
                    foreach (var item in res.productList)
                    {
                        var itemV = _mapper.Map<F5sVoucherBase>(item, opt => opt.AfterMap((s, d) => { d.productPartner = "GOTIT"; }));
                        if (item.size.Count > 0)
                        {
                            foreach (var iSize in item.size)
                            {
                                listV.Add(new F5sVoucherBase()
                                {
                                    brandLogo = itemV.brandLogo,
                                    brandNm = itemV.brandNm,
                                    productId = itemV.productId,
                                    productImg = itemV.productImg,
                                    productNm = itemV.productNm,
                                    productPartner = itemV.productPartner,
                                    productPrice = iSize.priceValue,
                                    productSize = iSize.priceId,
                                    productTyp = itemV.productTyp
                                });
                            }
                        }
                        else
                        {
                            listV.Add(itemV);
                        }
                    }
                    return new Application.Wrappers.Response<List<F5sVoucherBase>>(true, listV);
            }
            return new Application.Wrappers.Response<List<F5sVoucherBase>>(false, null, "Server Error");
        }

        public async Task<Application.Wrappers.Response<GotItTransCheckRes>> VoucherTransCheck(GotItTransCheckReq payload)
        {
            var content = new StringContent(JsonConvert.SerializeObject(payload, Formatting.Indented), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/transaction/check", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return new Application.Wrappers.Response<GotItTransCheckRes>(true, JsonConvert.DeserializeObject<GotItTransCheckRes>(jsonString));
            }
            var errorStr = await response.Content.ReadAsStringAsync();
            return new Application.Wrappers.Response<GotItTransCheckRes>(false, null, errorStr);
        }
    }
}
