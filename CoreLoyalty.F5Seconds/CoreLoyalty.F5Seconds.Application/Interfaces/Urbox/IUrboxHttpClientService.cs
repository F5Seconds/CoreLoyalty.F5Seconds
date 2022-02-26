using CoreLoyalty.F5Seconds.Application.DTOs.Urox;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.Urbox
{
    public interface IUrboxHttpClientService
    {
        Task<UrboxVoucherList> VoucherListAsync();
        Task<UrboxVoucherDetailData> VoucherDetailAsync(int id);
    }
}
