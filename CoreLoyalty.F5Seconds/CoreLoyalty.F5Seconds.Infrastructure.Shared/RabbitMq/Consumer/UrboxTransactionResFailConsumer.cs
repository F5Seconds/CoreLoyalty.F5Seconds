using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class UrboxTransactionResFailConsumer : IConsumer<UrboxTransactionResFail>
    {
        private readonly IUrboxTransResFailRepositoryAsync _urboxTransResFail;
        public UrboxTransactionResFailConsumer(IUrboxTransResFailRepositoryAsync urboxTransResFail)
        {
            _urboxTransResFail = urboxTransResFail;
        }
        public async Task Consume(ConsumeContext<UrboxTransactionResFail> context)
        {
            await _urboxTransResFail.AddAsync(context.Message);
        }
    }
}
