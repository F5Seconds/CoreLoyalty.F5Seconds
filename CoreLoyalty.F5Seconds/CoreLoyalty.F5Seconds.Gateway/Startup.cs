using CoreLoyalty.F5Seconds.Application;
using CoreLoyalty.F5Seconds.Application.Interfaces;
using CoreLoyalty.F5Seconds.Gateway.Extensions;
using CoreLoyalty.F5Seconds.Gateway.Services;
using CoreLoyalty.F5Seconds.Infrastructure.Identity;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence;
using CoreLoyalty.F5Seconds.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreLoyalty.F5Seconds.Gateway
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }

        public IConfiguration _config { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer();
            services.AddIdentityInfrastructure(_config);
            services.AddPersistenceInfrastructure(_config);
            services.AddSharedInfrastructure(_config);
            services.AddHttpClientExtension();
            services.AddSwaggerExtension();
            services.AddControllers();
            services.AddApiVersioningExtension();
            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CoreLoyalty.F5Seconds.Gateway v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwaggerExtension();
            app.UseHealthChecks("/health");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
