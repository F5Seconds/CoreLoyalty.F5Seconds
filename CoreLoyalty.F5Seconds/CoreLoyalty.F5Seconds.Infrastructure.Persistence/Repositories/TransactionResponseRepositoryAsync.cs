using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class TransactionResponseRepositoryAsync : GenericRepositoryAsync<GotItTransactionResponse>, IGotItTransResRepositoryAsync
    {
        private readonly DbSet<GotItTransactionResponse> _transactionResponse;
        public TransactionResponseRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _transactionResponse = dbContext.Set<GotItTransactionResponse>();
        }
    }
}
