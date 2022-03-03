using AutoMapper;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Urbox.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CoreLoyalty.F5Seconds.Urbox.Consumer
{
    public class UrboxVoucherNotUsedConsumer : IConsumer<UrboxTransactionResponse>
    {
        private readonly IUrboxHttpClientService _urboxHttpClient;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UrboxVoucherNotUsedConsumer> _logger;
        public UrboxVoucherNotUsedConsumer(
            IUrboxHttpClientService urboxHttpClient, 
            IConfiguration config, 
            IMapper mapper, 
            IWebHostEnvironment env, 
            IBus bus, ILogger<UrboxVoucherNotUsedConsumer> logger)
        {
            _urboxHttpClient = urboxHttpClient;
            _urboxHttpClient = urboxHttpClient;
            _bus = bus;
            _config = config;
            _env = env;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<UrboxTransactionResponse> context)
        {
            var response = await _urboxHttpClient.VoucherTransCheck(new UrboxTransCheckReq() { transaction_id = context.Message.TransactionId });
            if (response.Succeeded && response.Data.done.Equals(1))
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
                foreach (var item in response.Data.data.detail)
                {
                    await endPoint.Send(item);
                }
            }
        }
    }
}
