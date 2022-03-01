using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionResponseConsumer : IConsumer<TransactionResponse>
    {
        private ITransactionResponseRepositoryAsync _transactionResponse;
        public TransactionResponseConsumer(ITransactionResponseRepositoryAsync transactionResponse)
        {
            _transactionResponse = transactionResponse;
        }
        public async Task Consume(ConsumeContext<TransactionResponse> context)
        {
            await _transactionResponse.AddAsync(context.Message);
        }
    }
}
