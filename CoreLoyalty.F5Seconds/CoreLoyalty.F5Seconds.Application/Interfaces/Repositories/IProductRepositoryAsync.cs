using CoreLoyalty.F5Seconds.Domain.Entities;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<bool> IsUniqueBarcodeAsync(string barcode);
        Task<Product> IsUniqueProductAsync(int id, string partner,int size);
    }
}
