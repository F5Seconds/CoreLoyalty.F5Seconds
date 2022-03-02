using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItTransResFailRepositoryAsync : GenericRepositoryAsync<GotItTransactionResFail>, IGotItTransResFailRepositoryAsync
    {
        private readonly DbSet<GotItTransactionResFail> _gotItTransactionResFails;
        public GotItTransResFailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _gotItTransactionResFails = dbContext.Set<GotItTransactionResFail>();
        }
    }
}
