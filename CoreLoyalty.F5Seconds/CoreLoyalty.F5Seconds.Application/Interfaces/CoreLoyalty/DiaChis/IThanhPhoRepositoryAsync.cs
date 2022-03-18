using System.Collections.Generic;
using System.Threading.Tasks;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;

namespace CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis
{
    public interface IThanhPhoRepositoryAsync : IGenericRepositoryAsync<ThanhPho>
    {
        Task<IReadOnlyList<ThanhPho>> GetListAsync();
        Task<PagedList<ThanhPho>> GetPagedListAsync(GetAllThanhPhosParameter parameter);
        Task<ThanhPho> GetThanhPhoByIdAsync(int id);
        Task<bool> IsExitedByCode(string code);
        Task<bool> IsUniqueBarcodeAsync(string barcode);
        Task<PagedList<ThanhPho>> GetAllPagedListAsync(GetAllThanhPhosParameter parameter);

    }
}
