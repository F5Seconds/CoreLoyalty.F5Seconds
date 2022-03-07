using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using MassTransit;
using System.Threading.Tasks;
using static CoreLoyalty.F5Seconds.Application.DTOs.GotIt.GotItTransCheckRes;

namespace CoreLoyalty.F5Seconds.GotIt.Consumer
{
    public class GotItVoucherUpdateStatusConumer : IConsumer<GotItTransCheckResVoucher>
    {
        private readonly IGotItTransResSuccessRepositoryAsync _gotItTransRes;
        private readonly int[] NotUse =  { 0,1,2,3 };
        private readonly int[] Used = { 4 };
        private readonly int[] Expired = { 8 };
        private readonly int[] Canceled = { 9 };
        public GotItVoucherUpdateStatusConumer(IGotItTransResSuccessRepositoryAsync gotItTransRes)
        {
            _gotItTransRes = gotItTransRes;
        }
        public async Task Consume(ConsumeContext<GotItTransCheckResVoucher> context)
        {
            var message = context.Message;
            var voucher = await _gotItTransRes.FindByVoucherCode(message.voucherCode);
            if(voucher is not null)
            {
                voucher.Status = message.stateCode;
                voucher.UsedTime = message.used_time;
                voucher.UsedBrand = message.used_brand; 
                voucher.StateText = message.stateText;
                await _gotItTransRes.UpdateAsync(voucher);    
            }
        }
    }
}
