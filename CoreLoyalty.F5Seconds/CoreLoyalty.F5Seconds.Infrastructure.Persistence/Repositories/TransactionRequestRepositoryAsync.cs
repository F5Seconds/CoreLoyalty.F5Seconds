using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class TransactionRequestRepositoryAsync : GenericRepositoryAsync<GotItTransactionRequest>, IGotItTransReqRepositoryAsync
    {
        private readonly DbSet<GotItTransactionRequest> _transactionRequests;
        public TransactionRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _transactionRequests = dbContext.Set<GotItTransactionRequest>();
        }
    }
}
