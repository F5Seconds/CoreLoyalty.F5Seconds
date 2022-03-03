using CoreLoyalty.F5Seconds.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories
{
    public interface IGotItTransResSuccessRepositoryAsync : IGenericRepositoryAsync<GotItTransactionResponse>
    {
        Task<List<GotItTransactionResponse>> ListVoucherNotUsed();
        Task<GotItTransactionResponse> FindByVoucherCode(string code);
    }
}
