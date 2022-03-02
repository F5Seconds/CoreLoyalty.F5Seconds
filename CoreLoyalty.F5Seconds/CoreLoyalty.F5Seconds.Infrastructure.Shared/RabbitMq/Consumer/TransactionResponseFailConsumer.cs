using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionResponseFailConsumer : IConsumer<GotItTransactionResFail>
    {
        private IGotItTransResFailRepositoryAsync _transactionResFail;
        public TransactionResponseFailConsumer(IGotItTransResFailRepositoryAsync transactionResFail)
        {
            _transactionResFail = transactionResFail;
        }
        public async Task Consume(ConsumeContext<GotItTransactionResFail> context)
        {
            await _transactionResFail.AddAsync(context.Message);
        }
    }
}
