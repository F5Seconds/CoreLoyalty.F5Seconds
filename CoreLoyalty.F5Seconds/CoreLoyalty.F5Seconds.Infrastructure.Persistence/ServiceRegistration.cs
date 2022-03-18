using CoreLoyalty.F5Seconds.Application.Interfaces;
using CoreLoyalty.F5Seconds.Application.Interfaces.CoreLoyalty.DiaChis;
using CoreLoyalty.F5Seconds.Application.Interfaces.GotIt.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Application.Interfaces.Urbox.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.GotIt;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories.Urbox;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using VietCapital.Partner.F5Seconds.Infrastructure.Persistence.Repositories.CoreLoyalty.DiaChis;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration, bool isProduction)
        {
            string appConStr = configuration.GetConnectionString("DefaultConnection");
            if (isProduction)
            {
                appConStr = Environment.GetEnvironmentVariable("DB_URI_APPLICATION");
            }
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 35));
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                appConStr, serverVersion,
                b => {
                    b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    b.MigrationsAssembly("CoreLoyalty.F5Seconds.Gateway");
                }));
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            services.AddTransient<IMaLoiRepositoryAsync, MaLoiRepositoryAsync>();

            services.AddTransient<IGotItTransReqRepositoryAsync, GotItTransReqRepositoryAsync>();
            services.AddTransient<IGotItTransResSuccessRepositoryAsync, GotItTransResSuccessRepositoryAsync>();
            services.AddTransient<IGotItTransResFailRepositoryAsync, GotItTransResFailRepositoryAsync>();

            services.AddTransient<IUrboxTransReqRepositoryAsync, UrboxTransReqRepositoryAsync>();
            services.AddTransient<IUrboxTransResSuccessRepositoryAsync, UrboxTransResSuccessRepositoryAsync>();
            services.AddTransient<IUrboxTransResFailRepositoryAsync, UrboxTransResFailRepositoryAsync>();

            services.AddTransient<IThanhPhoRepositoryAsync, ThanhPhoRepositoryAsync>();

            #endregion
        }
    }
}
