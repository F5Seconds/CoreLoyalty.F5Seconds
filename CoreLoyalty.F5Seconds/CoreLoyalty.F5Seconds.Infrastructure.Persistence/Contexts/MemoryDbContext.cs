using CoreLoyalty.F5Seconds.Domain.MemoryModels;
using Microsoft.EntityFrameworkCore;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts
{
    public class MemoryDbContext: DbContext
    {
        public MemoryDbContext(DbContextOptions<MemoryDbContext> options) : base(options)
        {

        }
        public DbSet<ProductMemory> Products { get; set; }
    }
}
