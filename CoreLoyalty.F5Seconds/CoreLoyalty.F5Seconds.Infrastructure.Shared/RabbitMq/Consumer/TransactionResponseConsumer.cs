using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionResponseConsumer : IConsumer<GotItTransactionResponse>
    {
        private IGotItTransResRepositoryAsync _transactionResponse;
        public TransactionResponseConsumer(IGotItTransResRepositoryAsync transactionResponse)
        {
            _transactionResponse = transactionResponse;
        }
        public async Task Consume(ConsumeContext<GotItTransactionResponse> context)
        {
            await _transactionResponse.AddAsync(context.Message);
        }
    }
}
