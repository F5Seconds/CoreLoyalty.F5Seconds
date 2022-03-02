using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt
{
    public class GotItTransReqRepositoryAsync : GenericRepositoryAsync<GotItTransactionRequest>, IGotItTransReqRepositoryAsync
    {
        private readonly DbSet<GotItTransactionRequest> _gotItTransactionRequests;
        public GotItTransReqRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _gotItTransactionRequests = dbContext.Set<GotItTransactionRequest>();
        }
    }
}
