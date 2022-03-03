using AutoMapper;
using CoreLoyalty.F5Seconds.Application.Interfaces;
using CoreLoyalty.F5Seconds.GotIt.Extensions;
using CoreLoyalty.F5Seconds.GotIt.Services;
using CoreLoyalty.F5Seconds.Infrastructure.Identity;
using CoreLoyalty.F5Seconds.Infrastructure.Persistence;
using CoreLoyalty.F5Seconds.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CoreLoyalty.F5Seconds.GotIt
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<Startup> _logger;
        public IConfiguration _config { get; }
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _config = configuration;
            _env = env;
            using var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddConsole();
                builder.AddEventSourceLogger();
            });
            _logger = loggerFactory.CreateLogger<Startup>();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistenceInfrastructure(_config, _env.IsProduction());
            services.AddIdentityInfrastructure(_config, _env);
            services.AddSharedInfrastructure(_config);
            services.AddAutoMapper(typeof(Application.Mappings.GatewayProfile));
            services.AddHttpClientExtension(_config,_env);
            services.AddRabbitMqExtension(_config,_env);
            services.AddHostedService(); 
            services.AddSwaggerExtension();
            services.AddApiVersioningExtension();
            services.AddControllers();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerExtension();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
