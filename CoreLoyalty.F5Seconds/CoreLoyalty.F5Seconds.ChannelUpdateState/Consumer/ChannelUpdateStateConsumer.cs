using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
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
        public ChannelUpdateStateConsumer(IWebHostEnvironment env,
            IConfiguration config,
            IBus bus)
        {
            _env = env;
            _config = config;
            _bus = bus;
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
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);
        }
    }
}
