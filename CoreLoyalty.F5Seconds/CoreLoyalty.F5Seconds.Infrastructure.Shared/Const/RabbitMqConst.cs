using Microsoft.Extensions.Configuration;
using System;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.Const
{
    public static class RabbitMqConst
    {
        public static string Host { get; set; } = "RABBITMQ_HOST";
        public static string Vhost { get; set; } = "RABBITMQ_USER";
        public static string User { get; set; } = "RABBITMQ_USER";
        public static string Pass { get; set; } = "RABBITMQ_PASS";
        public static string TransRequest { get; set; } = "RABBITMQ_TRANS_REQUEST";
        public static string TransResSuccess { get; set; } = "RABBITMQ_TRANS_RES_SUCCESS";
        public static string TransResFail { get; set; } = "RABBITMQ_TRANS_RES_FAIL";
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
}
