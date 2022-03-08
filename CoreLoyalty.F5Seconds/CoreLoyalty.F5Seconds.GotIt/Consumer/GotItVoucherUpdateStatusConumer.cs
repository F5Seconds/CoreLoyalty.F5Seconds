using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Shared.Const;
using MassTransit;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.GotIt.GotItTransCheckRes;

namespace CoreLoyalty.F5Seconds.GotIt.Consumer
{
    public class GotItVoucherUpdateStatusConumer : IConsumer<GotItTransCheckResVoucher>
    {
        private readonly IGotItTransResSuccessRepositoryAsync _gotItTransRes;
        private int[] NotUse =  { 0,1,2,3,5,6,7 };
        private int[] Used = { 4 };
        private int[] Expired = { 8 };
        private int[] Canceled = { 9 };
        string rabbitHost = "";
        string rabbitvHost = "";
        private readonly IBus _bus;
        string channelUpdateStateQueue = "";
        private readonly IWebHostEnvironment _env;
        private IConfiguration _config;
        public GotItVoucherUpdateStatusConumer(
            IGotItTransResSuccessRepositoryAsync gotItTransRes, 
            IWebHostEnvironment env, 
            IConfiguration config, 
            IBus bus)
        {
            _gotItTransRes = gotItTransRes;
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
        public async Task Consume(ConsumeContext<GotItTransCheckResVoucher> context)
        {
            var message = context.Message;
            var voucher = await _gotItTransRes.FindByCodeAndTransId(message.voucherCode,message.transactionId);
            if(voucher is not null)
            {
                voucher.Status = message.stateCode;
                voucher.UsedTime = message.used_time;
                voucher.UsedBrand = message.used_brand; 
                voucher.StateText = message.stateText;
                await _gotItTransRes.UpdateAsync(voucher);
                if (!NotUse.Contains(message.stateCode))
                {
                    bool usedTime = DateTime.TryParseExact(message.used_time, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime usedTimeParse);
                    await SendChannelUpdateStateQueue(new ChannelUpdateStateDto()
                    {
                        Channel = message.channel,
                        ProductCode = message.productCode,
                        State = message.stateCode,
                        TransactionId = message.transactionId,
                        UsedBrand = message.used_brand,
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
