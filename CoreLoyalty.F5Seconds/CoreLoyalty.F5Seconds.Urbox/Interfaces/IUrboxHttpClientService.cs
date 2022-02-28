using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Urbox.Interfaces
{
    public interface IUrboxHttpClientService
    {
        Task<Response<List<F5sVoucherBase>>> VoucherListAsync();
        Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id);
        Task<Response<List<F5sVoucherCode>>> BuyVoucherAsync(UrboxBuyVoucherReq voucher);
    }
}
