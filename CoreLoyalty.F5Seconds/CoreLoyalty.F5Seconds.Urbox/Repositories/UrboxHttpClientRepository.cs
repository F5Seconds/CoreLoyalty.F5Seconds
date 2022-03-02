using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.Urox.UrboxBuyVocherRes.UrboxBuyVocherResData.UrboxBuyVoucherResCart;

namespace CoreLoyalty.F5Seconds.Urbox.Repositories
{
    public class UrboxHttpClientRepository : IUrboxHttpClientService
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IBus _bus;
        private string Partner = "URBOX";
        public UrboxHttpClientRepository(HttpClient client, IConfiguration config, IMapper mapper, IWebHostEnvironment env, IBus bus)
        {
            _bus = bus;
            _client = client;
            _config = config;
            _mapper = mapper;
            _env = env;
        }
        public async Task<Application.Wrappers.Response<List<F5sVoucherCode>>> BuyVoucherAsync(UrboxBuyVoucherReq voucher)
        {
            var transReq = _mapper.Map<GotItTransactionRequest>(voucher,opt => opt.AfterMap((s,d) => d.Partner = Partner ));
            var requestEndpoint = await _bus.GetSendEndpoint(RabbitMqConst.FormatUriRabbitMq(1,_env.IsProduction(),_config));
            var resSuccessEndpoint = await _bus.GetSendEndpoint(RabbitMqConst.FormatUriRabbitMq(2, _env.IsProduction(), _config));
            var resFailEndpoint = await _bus.GetSendEndpoint(RabbitMqConst.FormatUriRabbitMq(3, _env.IsProduction(), _config));
            
            var content = new StringContent(JsonConvert.SerializeObject(voucher), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/2.0/cart/cartPayVoucher?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UrboxMessageBase>(jsonString);
                if (result is not null && result.done.Equals(0))
                {
                    transReq.Status = 0;
                    await requestEndpoint.Send(transReq);
                    await resFailEndpoint.Send(new GotItTransactionResFail()
                    {
                        Code = result.done.ToString(),
                        Message = result.msg,
                        Partner = Partner,
                        TransactionId = transReq.TransactionId,
                        ProductId = transReq.PropductId,
                        Created = DateTime.Now
                    });
                    return new Application.Wrappers.Response<List<F5sVoucherCode>>(false, null, result.msg);
                }
                transReq.Status = 1;
                await requestEndpoint.Send(transReq);
                var resultV = JsonConvert.DeserializeObject<UrboxBuyVocherRes>(jsonString);
                return new Application.Wrappers.Response<List<F5sVoucherCode>>(true,FormatVoucherCode(voucher,resultV.data.cart.code_link_gift,resSuccessEndpoint));
            }
            transReq.Status = -1;
            await requestEndpoint.Send(transReq);
            await resFailEndpoint.Send(new GotItTransactionResFail()
            {
                Code = response.StatusCode.ToString(),
                Message = response.Content.ReadAsStringAsync().Result,
                Partner = Partner,
                TransactionId = transReq.TransactionId,
                ProductId = transReq.PropductId,
                Created = DateTime.Now
            });

            return new Application.Wrappers.Response<List<F5sVoucherCode>>(false,null, response.Content.ReadAsStringAsync().Result);
        }

        private List<F5sVoucherCode> FormatVoucherCode(UrboxBuyVoucherReq vReq,List<UrboxBuyVoucherResCode> vRes,ISendEndpoint endPoint)
        {
            List<F5sVoucherCode> f5SVoucherCodes = new List<F5sVoucherCode>();
            foreach (var v in vRes)
            {
                f5SVoucherCodes.Add(new F5sVoucherCode()
                {
                    customerPhone = vReq.ttphone,
                    expiryDate = v.expired,
                    productPrice = vReq.productPrice,
                    propductId = vReq.productCode,
                    transactionId = vReq.transaction_id,
                    voucherCode = v.code
                });
                endPoint.Send(new GotItTransactionResponse()
                {
                    Created = DateTime.Now,
                    CustomerPhone = vReq.ttphone,
                    ExpiryDate = v.expired,
                    ProductPrice = vReq.productPrice,
                    PropductId = vReq.productCode,
                    TransactionId = vReq.transaction_id,
                    VoucherCode = v.code
                }).Wait();
            }
            return f5SVoucherCodes;
        }

        public async Task<Application.Wrappers.Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var response = await _client.GetAsync($"/4.0/gift/detail?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}&lang=vi&id={id}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var p = JsonConvert.DeserializeObject<UrboxVoucherDetailData>(jsonString);
                return new Application.Wrappers.Response<F5sVoucherDetail>(true, _mapper.Map<F5sVoucherDetail>(p));
            }
            return new Application.Wrappers.Response<F5sVoucherDetail>(false, null, "Server Error");
        }

        public async Task<Application.Wrappers.Response<List<F5sVoucherBase>>> VoucherListAsync()
        {
            var response = await _client.GetAsync($"/4.0/gift/lists?app_id={_config["Urbox:AppId"]}&app_secret={_config["Urbox:AppSecret"]}");
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var vouchers = JsonConvert.DeserializeObject<UrboxVoucherList>(jsonString);
                var p = _mapper.Map<List<F5sVoucherBase>>(vouchers.data.items, opt => opt.AfterMap((s, d) =>
                {
                    foreach (var i in d)
                    {
                        i.productPartner = "URBOX";
                    }
                }));
                return new Application.Wrappers.Response<List<F5sVoucherBase>>(true,p);
            }
            return new Application.Wrappers.Response<List<F5sVoucherBase>>(false, null, "Server Error");
        }
    }
}
