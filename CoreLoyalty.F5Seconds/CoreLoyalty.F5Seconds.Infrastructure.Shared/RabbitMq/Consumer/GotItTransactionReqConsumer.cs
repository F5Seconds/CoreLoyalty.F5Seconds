using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class GotItTransactionReqConsumer : IConsumer<GotItTransactionRequest>
    {
        private IGotItTransReqRepositoryAsync _gotItTransactionReq;
        public GotItTransactionReqConsumer(IGotItTransReqRepositoryAsync transactionRequest)
        {
            _gotItTransactionReq = transactionRequest;   
        }

        public async Task Consume(ConsumeContext<GotItTransactionRequest> context)
        {
            await _gotItTransactionReq.AddAsync(context.Message);
        }
    }
}
