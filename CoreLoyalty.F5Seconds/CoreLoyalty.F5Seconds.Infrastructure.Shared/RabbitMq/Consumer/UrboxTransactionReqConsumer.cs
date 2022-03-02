using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using MassTransit;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Shared.RabbitMq.Consumer
{
    public class UrboxTransactionReqConsumer : IConsumer<UrboxTransactionRequest>
    {
        private readonly IUrboxTransReqRepositoryAsync _urboxTransReq;
        public UrboxTransactionReqConsumer(IUrboxTransReqRepositoryAsync urboxTransReq)
        {
            _urboxTransReq = urboxTransReq;
        }
        public async Task Consume(ConsumeContext<UrboxTransactionRequest> context)
        {
            await _urboxTransReq.AddAsync(context.Message);
        }
    }
}
