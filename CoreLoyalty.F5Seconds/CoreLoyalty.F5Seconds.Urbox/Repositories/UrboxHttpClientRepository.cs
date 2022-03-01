﻿using AutoMapper;
using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Enums;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.Settings;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
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
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IWebHostEnvironment _env;
        private readonly IBus _bus;
        private string Partner = "URBOX";
        public UrboxHttpClientRepository(HttpClient client, IConfiguration config, IMapper mapper, IOptions<RabbitMqSettings> rabbitMqSettings, IWebHostEnvironment env)
        {
            _client = client;
            _config = config;
            _mapper = mapper;
            _rabbitMqSettings = rabbitMqSettings.Value;
            _env = env;
        }
        public async Task<Application.Wrappers.Response<List<F5sVoucherCode>>> BuyVoucherAsync(UrboxBuyVoucherReq voucher)
        {
            var transReq = _mapper.Map<TransactionRequest>(voucher,opt => opt.AfterMap((s,d) => d.Partner = Partner ));
            var requestEndpoint = await _bus.GetSendEndpoint(FormatUriRabbitMq(1));
            var resSuccessEndpoint = await _bus.GetSendEndpoint(FormatUriRabbitMq(2));
            var resFailEndpoint = await _bus.GetSendEndpoint(FormatUriRabbitMq(3));
            
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
                    await resFailEndpoint.Send(new TransactionResFail()
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
            await resFailEndpoint.Send(new TransactionResFail()
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

        private Uri FormatUriRabbitMq(int queueType)
        {
            string rabbitHost = _rabbitMqSettings.Host;
            string rabbitvHost = _rabbitMqSettings.vHost;
            string requestQueue = _config["RabbitMqSettings:transactionReqQueue"];
            string responseSuccessQueue = _config["RabbitMqSettings:transactionResQueue"];
            string responseFailQueue = _config["RabbitMqSettings:transactionResFailQueue"];
            if (_env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_HOST.ToString());
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_VHOST.ToString());
                requestQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_REQ_QUEUE.ToString());
                responseSuccessQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_RES_SUCCESS_QUEUE.ToString());
                responseFailQueue = Environment.GetEnvironmentVariable(RabbitMqs.RABBITMQ_RES_FAIL_QUEUE.ToString());
            }
            switch (queueType)
            {
                case 1:
                    return new Uri($"{rabbitHost}/{rabbitvHost}/{requestQueue}");
                case 2:
                    return new Uri($"{rabbitHost}/{rabbitvHost}/{responseSuccessQueue}");
                default:
                    return new Uri($"{rabbitHost}/{rabbitvHost}/{responseFailQueue}");
            }
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
                endPoint.Send(new TransactionResponse()
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
