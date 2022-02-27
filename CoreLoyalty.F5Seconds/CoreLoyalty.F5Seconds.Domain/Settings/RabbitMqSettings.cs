using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLoyalty.F5Seconds.Domain.Settings
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public string vHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProductSyncQueue { get; set; }
    }
}
