using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.ChannelUpdateState.Interfaces;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.ChannelUpdateState.Consumer
{
    public class ChannelUpdateStateConsumer : IConsumer<ChannelUpdateStateDto>
    {
        string rabbitHost = "";
        string rabbitvHost = "";
        private readonly IBus _bus;
        private readonly IWebHostEnvironment _env;
        private IConfiguration _config;
        private readonly ILogger<ChannelUpdateStateConsumer> _logger;
        private readonly IVietCapitalHttpClientService _vietCapitalHttp;
        public ChannelUpdateStateConsumer(IWebHostEnvironment env,
            IConfiguration config,
            IBus bus,
            ILogger<ChannelUpdateStateConsumer> logger,
            IVietCapitalHttpClientService vietCapitalHttp)
        {
            _env = env;
            _config = config;
            _bus = bus;
            _logger = logger;
            _vietCapitalHttp = vietCapitalHttp;
            if (_env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Host);
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Vhost);
            }
            else
            {
                rabbitHost = _config[RabbitMqAppSettingConst.Host];
                rabbitvHost = _config[RabbitMqAppSettingConst.Vhost];
            }
        }
        public async Task Consume(ConsumeContext<ChannelUpdateStateDto> context)
        {
            var message = context.Message;
            Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{ChannelConst.DictionaryChannel[message.Channel].Queue}");
            await _vietCapitalHttp.PostVietCapitalStateUpdate(message);
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);
        }
    }
}
