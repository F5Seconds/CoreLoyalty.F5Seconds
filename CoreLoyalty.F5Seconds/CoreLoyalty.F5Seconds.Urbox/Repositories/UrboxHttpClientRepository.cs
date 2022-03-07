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
using System.Globalization;
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
        private string _partner = "URBOX";
        private string AppId = "";
        private string AppSecret = "";
        public UrboxHttpClientRepository(HttpClient client, IConfiguration config, IMapper mapper, IWebHostEnvironment env, IBus bus)
        {
            _bus = bus;
            _client = client;
            _config = config;
            _mapper = mapper;
            _env = env;
            if (_env.IsDevelopment())
            {
                AppId = _config["Urbox:AppId"];
                AppSecret = _config["Urbox:AppSecret"];
            }
            if (_env.IsProduction())
            {
                AppId = Environment.GetEnvironmentVariable("URBOX_APPID");
                AppSecret = Environment.GetEnvironmentVariable("URBOX_APPSECRET");
            }
        }
        public async Task<Application.Wrappers.Response<List<F5sVoucherCode>>> BuyVoucherAsync(UrboxBuyVoucherReq voucher)
        {
            string payloadBuyVoucher = JsonConvert.SerializeObject(voucher, Formatting.Indented);
            var transReq = _mapper.Map<UrboxTransactionRequest>(voucher,opt => opt.AfterMap((s,d) => { 
                d.Partner = _partner; 
                d.Payload = payloadBuyVoucher;
            }));
            var requestEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(1,_env.IsProduction(),_config));
            var resSuccessEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(2, _env.IsProduction(), _config));
            var resFailEndpoint = await _bus.GetSendEndpoint(RabbitMqEnvConst.FormatUriRabbitMq(3, _env.IsProduction(), _config));
            
            var content = new StringContent(payloadBuyVoucher, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/2.0/cart/cartPayVoucher?app_id={AppId}&app_secret={AppSecret}", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<UrboxMessageBase>(jsonString);
                if (result is not null && result.done.Equals(0))
                {
                    transReq.Status = 0;
                    await requestEndpoint.Send(transReq);
                    await resFailEndpoint.Send(new UrboxTransactionResFail()
                    {
                        Code = result.done.ToString(),
                        Message = result.msg,
                        Partner = _partner,
                        TransactionId = transReq.TransactionId,
                        ProductId = transReq.PropductId,
                        Payload = jsonString,
                        Created = DateTime.Now
                    });
                    return new Application.Wrappers.Response<List<F5sVoucherCode>>(false, null, result.msg);
                }
                transReq.Status = 1;
                await requestEndpoint.Send(transReq);
                var resultV = JsonConvert.DeserializeObject<UrboxBuyVocherRes>(jsonString);
                return new Application.Wrappers.Response<List<F5sVoucherCode>>(true,FormatVoucherCode(voucher,resultV.data.cart.code_link_gift,resSuccessEndpoint,jsonString));
            }
            transReq.Status = -1;
            await requestEndpoint.Send(transReq);
            var errorStr = response.Content.ReadAsStringAsync().Result;
            await resFailEndpoint.Send(new GotItTransactionResFail()
            {
                Code = response.StatusCode.ToString(),
                Message = errorStr,
                Partner = _partner,
                TransactionId = transReq.TransactionId,
                ProductId = transReq.PropductId,
                Created = DateTime.Now
            });

            return new Application.Wrappers.Response<List<F5sVoucherCode>>(false,null, errorStr);
        }

        private List<F5sVoucherCode> FormatVoucherCode(UrboxBuyVoucherReq vReq,List<UrboxBuyVoucherResCode> vRes,ISendEndpoint endPoint,string payload)
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

                bool expried = DateTime.TryParseExact(v.expired, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None,out DateTime expiryDate);
                endPoint.Send(new UrboxTransactionResponse()
                {
                    Created = DateTime.Now,
                    CustomerPhone = vReq.ttphone,
                    ExpiryDate = expried ? expiryDate : null,
                    ProductPrice = vReq.productPrice,
                    PropductId = vReq.productCode,
                    TransactionId = vReq.transaction_id,
                    VoucherCode = v.code,
                    Payload = payload,
                    CodeDisplay = v.code_display,
                    CodeDisplayType = v.code_display_type,
                    CodeImage = v.code_image,
                    EstimateDelivery =  v.estimateDelivery,
                    Token = v.token,
                    Link = v.link,
                    DeliveryNote = v.delivery_note,
                    Pin = v.pin,
                    Address = v.ttaddress,
                    CityId = v.city_id,
                    DistrictId = v.district_id,
                    Email = v.ttemail,
                    Phone = v.ttphone,
                    WardId = v.ward_id,
                    Type = vReq.productType
                }).Wait();
            }
            return f5SVoucherCodes;
        }

        public async Task<Application.Wrappers.Response<F5sVoucherDetail>> VoucherDetailAsync(int id)
        {
            var response = await _client.GetAsync($"/4.0/gift/detail?app_id={AppId}&app_secret={AppSecret}&lang=vi&id={id}");
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
            var response = await _client.GetAsync($"/4.0/gift/lists?app_id={AppId}&app_secret={AppSecret}");
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

        public async Task<Application.Wrappers.Response<UrboxTransCheckRes>> VoucherTransCheck(UrboxTransCheckReq payload)
        {
            string payloadTransCheck = JsonConvert.SerializeObject(payload, Formatting.Indented);
            var content = new StringContent(payloadTransCheck, Encoding.UTF8, "application/json");
            var response = await _client.PostAsync($"/2.0/cart/getByTransaction?app_id={AppId}&app_secret={AppSecret}", content);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return new Application.Wrappers.Response<UrboxTransCheckRes>(true, JsonConvert.DeserializeObject<UrboxTransCheckRes>(jsonString));
            }
            var errorStr = await response.Content.ReadAsStringAsync();
            return new Application.Wrappers.Response<UrboxTransCheckRes>(false, null, errorStr);
        }
    }
}
