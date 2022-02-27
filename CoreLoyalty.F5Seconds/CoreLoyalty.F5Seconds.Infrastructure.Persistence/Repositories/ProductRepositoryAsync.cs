using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public Task<bool> IsUniqueBarcodeAsync(string code)
        {
            return _products
                .AllAsync(p => p.Code != code);
        }

        public async Task<Product> IsUniqueProductAsync(int id, string partner, int size)
        {
            return await _products.SingleOrDefaultAsync(p => p.ProductId.Equals(id) && p.Partner.Equals(partner) && p.Size.Equals(size));
        }
    }
}
