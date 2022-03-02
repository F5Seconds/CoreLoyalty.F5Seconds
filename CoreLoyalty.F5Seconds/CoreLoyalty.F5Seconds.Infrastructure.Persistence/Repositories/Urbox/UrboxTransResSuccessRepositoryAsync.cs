using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class UrboxTransResSuccessRepositoryAsync : GenericRepositoryAsync<UrboxTransactionResponse>, IUrboxTransResSuccessRepositoryAsync
    {
        private readonly DbSet<UrboxTransactionResponse> _urboxTransactionResponses;
        public UrboxTransResSuccessRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _urboxTransactionResponses = dbContext.Set<UrboxTransactionResponse>();
        }
    }
}
