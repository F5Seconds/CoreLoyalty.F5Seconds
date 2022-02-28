using CoreLoyalty.F5Seconds.Application.DTOs.F5seconds;
using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.Urbox
{
    public interface IUrboxHttpClientExternalService
    {
        Task<Response<List<F5sVoucherBase>>> VoucherListAsync();
        Task<Response<F5sVoucherDetail>> VoucherDetailAsync(int id);
        Task<UrboxBuyVocherRes> BuyVoucherAsync(UrboxBuyVoucherReq voucher);
    }
}
