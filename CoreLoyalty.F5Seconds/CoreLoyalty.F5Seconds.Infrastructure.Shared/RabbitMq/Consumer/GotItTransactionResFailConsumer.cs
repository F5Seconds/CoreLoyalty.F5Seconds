using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class GotItTransactionResFailConsumer : IConsumer<GotItTransactionResFail>
    {
        private IGotItTransResFailRepositoryAsync _gotItTransactionResFail;
        public GotItTransactionResFailConsumer(IGotItTransResFailRepositoryAsync gotItTransactionResFail)
        {
            _gotItTransactionResFail = gotItTransactionResFail;
        }
        public async Task Consume(ConsumeContext<GotItTransactionResFail> context)
        {
            await _gotItTransactionResFail.AddAsync(context.Message);
        }
    }
}
