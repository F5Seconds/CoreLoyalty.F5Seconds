using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CoreLoyalty.F5Seconds.VietCapital.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientExtension(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            string gotItUri = configuration["PartnerUri:GotIt"];
            if (env.IsProduction())
            {

            }
        }
    }
}
