using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.Urox.UrboxTransCheckRes.UrboxTransCheckResData;

namespace CoreLoyalty.F5Seconds.Urbox.Consumer
{
    public class UrboxVoucherUpdateStatusConumer : IConsumer<UrboxTransCheckResDataDetail>
    {
        private int[] NotUse = { 0,1,6,7,8,10 };
        private int[] Used = { 2,3 };
        private int[] Expired = { 4,5 };
        private int[] Canceled = { 9,11 };
        private readonly IUrboxTransResSuccessRepositoryAsync _urboxTransRes;
        string rabbitHost = "";
        string rabbitvHost = "";
        private readonly IBus _bus;
        string channelUpdateStateQueue = "";
        private readonly IWebHostEnvironment _env;
        private IConfiguration _config;
        public UrboxVoucherUpdateStatusConumer(
            IUrboxTransResSuccessRepositoryAsync urboxTransRes,
            IWebHostEnvironment env,
            IConfiguration config,
            IBus bus)
        {
            _urboxTransRes = urboxTransRes;
            _env = env;
            _config = config;
            _bus = bus;
            if (_env.IsProduction())
            {
                rabbitHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Host);
                rabbitvHost = Environment.GetEnvironmentVariable(RabbitMqEnvConst.Vhost);
                channelUpdateStateQueue = Environment.GetEnvironmentVariable(RabbitMqEnvConst.ChannelUpdateState);
            }
            else
            {
                rabbitHost = _config[RabbitMqAppSettingConst.Host];
                rabbitvHost = _config[RabbitMqAppSettingConst.Vhost];
                channelUpdateStateQueue = _config[RabbitMqAppSettingConst.ChannelUpdateState];
            }
        }
        public async Task Consume(ConsumeContext<UrboxTransCheckResDataDetail> context)
        {
            var message = context.Message;
            var voucher = await _urboxTransRes.FindByCodeAndTransId(message.code,message.transactionId);
            if (voucher is not null)
            {
                voucher.Status = message.deliveryCode??0;
                voucher.DeliveryNote = message.delivery_note;
                voucher.UsedTime = message.using_time;
                await _urboxTransRes.UpdateAsync(voucher);
                if (!NotUse.Contains(message.deliveryCode??0))
                {
                    bool usedTime = DateTime.TryParseExact(message.using_time, "dd/MM/yyyy", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime usedTimeParse);
                    await SendChannelUpdateStateQueue(new ChannelUpdateStateDto()
                    {
                        Channel = message.channel,
                        ProductCode = message.productCode,
                        State = message.deliveryCode??0,
                        TransactionId = message.transactionId,
                        UsedBrand = null,
                        UsedTime = usedTime ? usedTimeParse : null
                    });
                }
            }
        }

        public async Task SendChannelUpdateStateQueue(ChannelUpdateStateDto channel)
        {
            if (Used.Contains(channel.State)) channel.State = 2;
            else if (Expired.Contains(channel.State)) channel.State = 3;
            else if (Canceled.Contains(channel.State)) channel.State = 4;
            Uri uri = new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{channelUpdateStateQueue}");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(channel);
        }
    }
}
