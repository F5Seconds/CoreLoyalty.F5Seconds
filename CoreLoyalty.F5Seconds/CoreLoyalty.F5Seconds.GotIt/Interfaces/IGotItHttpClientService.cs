using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.GotIt.Interfaces
{
    public interface IGotItHttpClientService
    {
        Task<Response<List<F5sVoucherBase>>> VoucherListAsync();
        Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id);
        Task<Response<List<GotItBuyVoucherRes>>> BuyVoucherAsync(GotItBuyVoucherReq voucher);
    }
}
