using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class GotItTransactionResSuccessConsumer : IConsumer<GotItTransactionResponse>
    {
        private IGotItTransResSuccessRepositoryAsync _gotIttransactionResSuccess;
        public GotItTransactionResSuccessConsumer(IGotItTransResSuccessRepositoryAsync transactionResponse)
        {
            _gotIttransactionResSuccess = transactionResponse;
        }
        public async Task Consume(ConsumeContext<GotItTransactionResponse> context)
        {
            await _gotIttransactionResSuccess.AddAsync(context.Message);
        }
    }
}
