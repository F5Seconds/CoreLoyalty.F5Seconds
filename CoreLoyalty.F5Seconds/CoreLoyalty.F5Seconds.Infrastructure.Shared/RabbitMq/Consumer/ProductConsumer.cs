using CoreLoyalty.F5Seconds.Shared.RabbitMq.Publisher;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Shared.RabbitMq.Consumer
{
    public class ProductConsumer : IConsumer<ProductPublisher>
    {
        public Task Consume(ConsumeContext<ProductPublisher> context)
        {
            throw new NotImplementedException();
        }
    }
}
