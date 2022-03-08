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
using System.Collections.Generic;
using System.Linq;

namespace CoreLoyalty.F5Seconds.Urbox.Consumer
{
    public class UrboxVoucherNotUsedConsumer : IConsumer<UrboxTransactionResponse>
    {
        private readonly IUrboxHttpClientService _urboxHttpClient;
        private readonly IBus _bus;
        private readonly IConfiguration _config;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<UrboxVoucherNotUsedConsumer> _logger;
        public Random r = new Random();
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
            var transResponse = context.Message;
            var response = await _urboxHttpClient.VoucherTransCheck(new UrboxTransCheckReq() { transaction_id = transResponse.TransactionId });
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
                    item.deliveryCode = r.Next(0, 11);
                    item.using_time = DateTime.Now.ToString("dd/MM/yyyy");
                    item.channel = transResponse.Channel;
                    item.transactionId = transResponse.TransactionId;
                    item.productCode = transResponse.ProductCode;
                    await endPoint.Send(item);
                }
            }
        }

        private int GiveMeANumber()
        {
            var exclude = new HashSet<int>() { 3, 5 };
            var range = Enumerable.Range(1, 11).Where(i => !exclude.Contains(i));

            var rand = new Random();
            int index = rand.Next(1, 11 - exclude.Count);
            return range.ElementAt(index);
        }
    }
}
