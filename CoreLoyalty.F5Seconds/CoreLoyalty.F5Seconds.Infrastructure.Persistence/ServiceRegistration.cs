using CoreLoyalty.F5Seconds.Application.Interfaces;
using CoreLoyalty.F5Seconds.Application.Interfaces.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Contexts;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repositories;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace CoreLoyalty.F5Seconds.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<MemoryDbContext>(options =>
                options.UseInMemoryDatabase("MemoryDbContext"), ServiceLifetime.Scoped, ServiceLifetime.Scoped);
            var serverVersion = new MySqlServerVersion(new Version(5, 7, 35));
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySql(
                configuration.GetConnectionString("DefaultConnection"), serverVersion,
                b => {
                    b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
                    b.MigrationsAssembly("CoreLoyalty.F5Seconds.Gateway");
                }));
            #region Repositories
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IProductRepositoryAsync, ProductRepositoryAsync>();
            #endregion
        }
    }
}
