using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionRequestConsumer : IConsumer<TransactionRequest>
    {
        private ITransactionRequestRepositoryAsync _transactionRequest;
        public TransactionRequestConsumer(ITransactionRequestRepositoryAsync transactionRequest)
        {
            _transactionRequest = transactionRequest;   
        }

        public async Task Consume(ConsumeContext<TransactionRequest> context)
        {
            await _transactionRequest.AddAsync(context.Message);
        }
    }
}
