using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class UrboxTransactionResSuccessConsumer : IConsumer<UrboxTransactionResponse>
    {
        private readonly IUrboxTransResSuccessRepositoryAsync _urboxTransRes;
        public UrboxTransactionResSuccessConsumer(IUrboxTransResSuccessRepositoryAsync urboxTransRes)
        {
            _urboxTransRes = urboxTransRes;
        }
        public async Task Consume(ConsumeContext<UrboxTransactionResponse> context)
        {
            await _urboxTransRes.AddAsync(context.Message);
        }
    }
}
