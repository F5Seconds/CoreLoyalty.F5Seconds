using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using MassTransit;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.Urox.UrboxTransCheckRes.UrboxTransCheckResData;

namespace CoreLoyalty.F5Seconds.Urbox.Consumer
{
    public class UrboxVoucherUpdateStatusConumer : IConsumer<UrboxTransCheckResDataDetail>
    {
        private readonly IUrboxTransResSuccessRepositoryAsync _urboxTransRes;
        public UrboxVoucherUpdateStatusConumer(IUrboxTransResSuccessRepositoryAsync urboxTransRes)
        {
            _urboxTransRes = urboxTransRes;
        }
        public async Task Consume(ConsumeContext<UrboxTransCheckResDataDetail> context)
        {
            var message = context.Message;
            var voucher = await _urboxTransRes.FindByVoucherCode(message.code);
            if (voucher is not null)
            {
                voucher.Status = message.deliveryCode??0;
                voucher.DeliveryNote = message.delivery_note;
                await _urboxTransRes.UpdateAsync(voucher);
            }
        }
    }
}
