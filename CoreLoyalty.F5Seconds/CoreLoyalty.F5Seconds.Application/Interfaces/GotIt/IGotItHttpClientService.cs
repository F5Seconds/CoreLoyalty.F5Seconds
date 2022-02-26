using CoreLoyalty.F5Seconds.Application.DTOs.GotIt;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.GotIt
{
    public interface IGotItHttpClientService
    {
        Task<GotItVoucherList> VoucherListAsync();
        Task<GotItVoucherDetail> VoucherDetailAsync(int id);
    }
}
