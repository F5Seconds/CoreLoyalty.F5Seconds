using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.GotIt
{
    public interface IGotItHttpClientService
    {
        Task<Response<GotItVoucherList>> VoucherListAsync();
        Task<Response<GotItVoucherDetail>> VoucherDetailAsync(int id);
        Task<Response<List<GotItBuyVoucherRes>>> BuyVoucherAsync(GotItBuyVoucherReq voucher);
    }
}
