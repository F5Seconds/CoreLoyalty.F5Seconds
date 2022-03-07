using Microsoft.Extensions.Configuration;
using System;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.Const
{
    public static class RabbitMqEnvConst
    {
        public static string Host { get; set; } = "RABBITMQ_HOST";
        public static string Vhost { get; set; } = "RABBITMQ_VHOST";
        public static string User { get; set; } = "RABBITMQ_USER";
        public static string Pass { get; set; } = "RABBITMQ_PASS";
        public static string TransRequest { get; set; } = "RABBITMQ_TRANS_REQUEST";
        public static string TransResSuccess { get; set; } = "RABBITMQ_TRANS_RES_SUCCESS";
        public static string TransResFail { get; set; } = "RABBITMQ_TRANS_RES_FAIL";
        public static string VoucherNotUsed { get; set; } = "RABBITMQ_VOUCHER_NOTUSE";
        public static string VoucherUpdateStatus { get; set; } = "RABBITMQ_VOUCHER_UPDATESTATUS";
        public static string ChannelUpdateState { get; set; } = "RABBITMQ_CHANNEL_UPATE_STATE";
        public static Uri FormatUriRabbitMq(int queueType, bool isProduction, IConfiguration _config)
        {
            string rabbitHost = _config["RabbitMqSettings:Host"];
            string rabbitvHost = _config["RabbitMqSettings:vHost"];
            string rabbitTransReqQueue = _config["RabbitMqSettings:transactionReqQueue"];
            string rabbitTransResQueue = _config["RabbitMqSettings:transactionResQueue"];
            string rabbitTransResFailQueue = _config["RabbitMqSettings:transactionResFailQueue"];
            if (isProduction)
            {
                rabbitHost = Environment.GetEnvironmentVariable(Host);
                rabbitvHost = Environment.GetEnvironmentVariable(Vhost);
                rabbitTransReqQueue = Environment.GetEnvironmentVariable(TransRequest);
                rabbitTransResQueue = Environment.GetEnvironmentVariable(TransResSuccess);
                rabbitTransResFailQueue = Environment.GetEnvironmentVariable(TransResFail);
            }
            switch (queueType)
            {
                case 1:
                    return new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{rabbitTransReqQueue}");
                case 2:
                    return new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{rabbitTransResQueue}");
                default:
                    return new Uri($"rabbitmq://{rabbitHost}/{rabbitvHost}/{rabbitTransResFailQueue}");
            }
        }
    }

    public static class RabbitMqAppSettingConst
    {
        public static string Host { get; set; } = "RabbitMqSettings:Host";
        public static string Vhost { get; set; } = "RabbitMqSettings:vHost";
        public static string User { get; set; } = "RabbitMqSettings:Username";
        public static string Pass { get; set; } = "RabbitMqSettings:Password";
        public static string TransRequest { get; set; } = "RabbitMqSettings:transactionReqQueue";
        public static string TransResSuccess { get; set; } = "RabbitMqSettings:transactionResQueue";
        public static string TransResFail { get; set; } = "RabbitMqSettings:transactionResFailQueue";
        public static string VoucherNotUsed { get; set; } = "RabbitMqSettings:voucherNotUsedQueue";
        public static string VoucherUpdateStatus { get; set; } = "RabbitMqSettings:voucherUpdateStatusQueue";
        public static string ChannelUpdateState { get; set; } = "RabbitMqSettings:channelUpdateStateQueue";
    }
}
