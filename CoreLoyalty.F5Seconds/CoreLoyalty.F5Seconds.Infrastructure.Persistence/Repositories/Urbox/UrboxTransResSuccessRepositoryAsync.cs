using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class UrboxTransResSuccessRepositoryAsync : GenericRepositoryAsync<UrboxTransactionResponse>, IUrboxTransResSuccessRepositoryAsync
    {
        private readonly DbSet<UrboxTransactionResponse> _urboxTransactionResponses;
        public UrboxTransResSuccessRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _urboxTransactionResponses = dbContext.Set<UrboxTransactionResponse>();
        }

        public async Task<UrboxTransactionResponse> FindByVoucherCode(string code)
        {
            return await _urboxTransactionResponses.SingleOrDefaultAsync(x => x.VoucherCode.Equals(code));
        }

        public async Task<List<UrboxTransactionResponse>> ListVoucherNotUsed()
        {
            int[] notUsed = { 2,4, 11 };
            return await _urboxTransactionResponses.Where(x => !notUsed.Contains(x.Status)).ToListAsync();
        }
    }
}
