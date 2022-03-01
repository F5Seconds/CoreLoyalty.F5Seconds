using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class TransactionRequestRepositoryAsync : GenericRepositoryAsync<TransactionRequest>, ITransactionRequestRepositoryAsync
    {
        private readonly DbSet<TransactionRequest> _transactionRequests;
        public TransactionRequestRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _transactionRequests = dbContext.Set<TransactionRequest>();
        }
    }
}
