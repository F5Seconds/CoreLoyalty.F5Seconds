using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class MaLoiRepositoryAsync : GenericRepositoryAsync<MaLoi>, IMaLoiRepositoryAsync
    {
        private readonly DbSet<MaLoi> _maLoi;
        public MaLoiRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _maLoi = dbContext.Set<MaLoi>();
        }

        public async Task<MaLoi> FindByMaLoiGotIt(string maGotIt)
        {
            return await _maLoi.SingleOrDefaultAsync(x => x.MaGotIt.Equals(maGotIt));
        }

        public async Task<MaLoi> FindByMaLoiUrbox(string maUrbox)
        {
            return await _maLoi.SingleOrDefaultAsync(x => x.MaUrbox.Equals(maUrbox));
        }
    }
}
