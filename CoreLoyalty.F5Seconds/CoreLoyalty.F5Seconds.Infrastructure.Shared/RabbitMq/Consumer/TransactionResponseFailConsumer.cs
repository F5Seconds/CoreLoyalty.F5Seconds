using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionResponseFailConsumer : IConsumer<TransactionResFail>
    {
        private ITransactionResFailRepositoryAsync _transactionResFail;
        public TransactionResponseFailConsumer(ITransactionResFailRepositoryAsync transactionResFail)
        {
            _transactionResFail = transactionResFail;
        }
        public async Task Consume(ConsumeContext<TransactionResFail> context)
        {
            await _transactionResFail.AddAsync(context.Message);
        }
    }
}
