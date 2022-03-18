using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreLoyalty.F5Seconds.Application.Features.CoreLoyalty.DiaChis.ThanhPhos.Commands.GetAllThanhPhos;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Wrappers;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;

namespace VietCapital.Partner.F5Seconds.Infrastructure.Persistence.Repositories.CoreLoyalty.DiaChis
{
    public class ThanhPhoRepositoryAsync : GenericRepositoryAsync<ThanhPho>, IThanhPhoRepositoryAsync
    {
        private readonly DbSet<ThanhPho> _ThanhPhos;
        public ThanhPhoRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _ThanhPhos = dbContext.Set<ThanhPho>();
        }
        public async Task<IReadOnlyList<ThanhPho>> GetListAsync()
        {
            return await _ThanhPhos
                .Where(p => p.TrangThai)
                .ToListAsync();
        }

        private void Search(ref IQueryable<ThanhPho> ThanhPhos, string search)
        {
            if (string.IsNullOrWhiteSpace(search)) return;
            search = $"%{search.Trim()}%";
            ThanhPhos = ThanhPhos.Where(x =>
                EF.Functions.Like(x.Ten, search)
            );
        }

        public async Task<PagedList<ThanhPho>> GetPagedListAsync(GetAllThanhPhosParameter parameter)
        {
            var ThanhPhos = _ThanhPhos
                .Where(p => p.TrangThai).AsQueryable();
            Search(ref ThanhPhos,parameter.Search);
            return await PagedList<ThanhPho>.ToPagedList(ThanhPhos.OrderByDescending(x => x.Id), parameter.PageNumber, parameter.PageSize);
        }

        public async Task<bool> IsExitedByCode(string code)
        {
            return await _ThanhPhos.AnyAsync(x => x.Ten.Equals(code.Trim()));
        }

        public Task<bool> IsUniqueBarcodeAsync(string barcode)
        {
            return _ThanhPhos
                .AnyAsync(p => p.Ten == barcode);
        }

        public async Task<PagedList<ThanhPho>> GetAllPagedListAsync(GetAllThanhPhosParameter parameter)
        {
            var ThanhPhos = _ThanhPhos.AsQueryable();
            Search(ref ThanhPhos,parameter.Search);
            return await PagedList<ThanhPho>.ToPagedList(ThanhPhos.OrderByDescending(x => x.Id), parameter.PageNumber, parameter.PageSize);
        }

        public async Task<ThanhPho> GetThanhPhoByIdAsync(int id)
        {
           return await _ThanhPhos
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
