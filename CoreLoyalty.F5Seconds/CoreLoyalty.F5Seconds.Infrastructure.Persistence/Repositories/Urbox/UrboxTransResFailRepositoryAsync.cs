using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox
{
    public class UrboxTransResFailRepositoryAsync : GenericRepositoryAsync<UrboxTransactionResFail>, IUrboxTransResFailRepositoryAsync
    {
        private readonly DbSet<UrboxTransactionResFail> _urboxTransactionResFail;
        public UrboxTransResFailRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _urboxTransactionResFail = dbContext.Set<UrboxTransactionResFail>();
        }
    }
}
