﻿using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.GotIt.Interfaces;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.GotIt.Consumer
{
    public class GotItVoucherNotUsedConsumer : IConsumer<GotItTransactionResponse>
    {
        private readonly IGotItHttpClientService _gotItHttpClient;
        private readonly ILogger<GotItVoucherNotUsedConsumer> _logger;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        public GotItVoucherNotUsedConsumer(IGotItHttpClientService gotItHttpClient, ILogger<GotItVoucherNotUsedConsumer> logger, IBus bus, IConfiguration config, IWebHostEnvironment env)
        {
            _gotItHttpClient = gotItHttpClient; 
            _logger = logger;
            _bus = bus;
            _config = config;
            _env = env;
        }
        public async Task Consume(ConsumeContext<GotItTransactionResponse> context)
        {
            
            var response = await _gotItHttpClient.VoucherTransCheck(new GotItTransCheckReq() { voucherRefId = context.Message.TransactionId});
            if (response.Succeeded && response.Data.voucher is not null)
            {
                string rabbitHost = _config[RabbitMqAppSettingConst.Host];
                string rabbitvHost = _config[RabbitMqAppSettingConst.Vhost];
                string voucherUpdateStatus = _config[RabbitMqAppSettingConst.VoucherUpdateStatus];
                if (_env.IsProduction())
                {
                    rabbitHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Host);
                    rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Vhost);
                    voucherUpdateStatus = Environment.GetEnvironmentVariable(RabbitMqEnvConst.VoucherUpdateStatus);
                }
                Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{voucherUpdateStatus}");
                var endPoint = await _bus.GetSendEndpoint(uri);
                await endPoint.Send(response.Data.voucher);   
            }
        }
    }
}