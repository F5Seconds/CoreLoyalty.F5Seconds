using CoreLoyalty.F5Seconds.Application.Interfaces;
using CoreLoyalty.F5Seconds.Domain.Common;
using CoreLoyalty.F5Seconds.Domain.Entities;
using CoreLoyalty.F5Seconds.Domain.Entities.CuaHangs;
using CoreLoyalty.F5Seconds.Domain.Entities.DiaChis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Product> Products { get; set; }
        #region GotIt
        public DbSet<GotItTransactionRequest> GotItTransactionRequests { get; set; }
        public DbSet<GotItTransactionResponse> GotItTransactionResponses { get; set; }
        public DbSet<GotItTransactionResFail> GotItTransactionResFails { get; set; }
        #endregion

        #region Urbox
        public DbSet<UrboxTransactionRequest> UrboxTransactionRequests { get; set; }
        public DbSet<UrboxTransactionResponse> UrboxTransactionResponses { get; set; }
        public DbSet<UrboxTransactionResFail> UrboxTransactionResFails { get; set; }
        #endregion

        #region DiaChi
        public DbSet<ThanhPho> ThanhPhos { get; set; }
        public DbSet<QuanHuyen> QuanHuyens { get; set; }
        public DbSet<PhuongXa> PhuongXas { get; set; }
        #endregion

        #region CuaHang
        public DbSet<CuaHang> CuaHangs { get; set; }
        public DbSet<LinhVuc> LinhVucs { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        #endregion
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }
            base.OnModelCreating(builder);
        }
    }
}
