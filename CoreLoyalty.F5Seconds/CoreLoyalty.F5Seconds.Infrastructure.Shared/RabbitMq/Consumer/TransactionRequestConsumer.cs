using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class TransactionRequestConsumer : IConsumer<GotItTransactionRequest>
    {
        private IGotItTransReqRepositoryAsync _transactionRequest;
        public TransactionRequestConsumer(IGotItTransReqRepositoryAsync transactionRequest)
        {
            _transactionRequest = transactionRequest;   
        }

        public async Task Consume(ConsumeContext<GotItTransactionRequest> context)
        {
            await _transactionRequest.AddAsync(context.Message);
        }
    }
}
