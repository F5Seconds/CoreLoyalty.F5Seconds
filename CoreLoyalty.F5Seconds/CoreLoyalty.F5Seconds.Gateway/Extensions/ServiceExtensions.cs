using CoreLoyalty.F5Seconds.Gateway.HttpClients;
using Microsoft.Extensions.DependencyInjection;

namespace CoreLoyalty.F5Seconds.Gateway.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddHttpClientExtension(this IServiceCollection services)
        {
            services.AddHttpClient<IGotItClient,GotItClient>(c => { 
                c.BaseAddress = new System.Uri("https://localhost:5001"); 
            });
            services.AddHttpClient<UrboxClient,UrboxClient>(c => { 
                c.BaseAddress = new System.Uri("https://localhost:5004"); 
            });
        }
    }
}
