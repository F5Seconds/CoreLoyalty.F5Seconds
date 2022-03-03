using CoreLoyalty.F5Seconds.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories
{
    public interface IUrboxTransResSuccessRepositoryAsync : IGenericRepositoryAsync<UrboxTransactionResponse>
    {
        Task<List<UrboxTransactionResponse>> ListVoucherNotUsed();
        Task<UrboxTransactionResponse> FindByVoucherCode(string code);
    }
}
