using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItTransResSuccessRepositoryAsync : GenericRepositoryAsync<GotItTransactionResponse>, IGotItTransResSuccessRepositoryAsync
    {
        private readonly DbSet<GotItTransactionResponse> _gotItTransactionResponses;
        public GotItTransResSuccessRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _gotItTransactionResponses = dbContext.Set<GotItTransactionResponse>();
        }

        public async Task<GotItTransactionResponse> FindByVoucherCode(string code)
        {
            return await _gotItTransactionResponses.SingleOrDefaultAsync(x => x.VoucherCode.Equals(code));
        }

        public async Task<List<GotItTransactionResponse>> ListVoucherNotUsed()
        {
            int[] notUsed = {0,1,2,3,4};
            return await _gotItTransactionResponses.Where(x => notUsed.Contains(x.Status)).ToListAsync();
        }
    }
}
