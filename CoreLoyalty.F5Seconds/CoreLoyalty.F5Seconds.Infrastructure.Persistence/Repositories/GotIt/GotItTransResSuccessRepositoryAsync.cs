using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItTransResSuccessRepositoryAsync : GenericRepositoryAsync<GotItTransactionResponse>, IGotItTransResSuccessRepositoryAsync
    {
        private readonly DbSet<GotItTransactionResponse> _gotItTransactionResponses;
        public GotItTransResSuccessRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _gotItTransactionResponses = dbContext.Set<GotItTransactionResponse>();
        }
    }
}
