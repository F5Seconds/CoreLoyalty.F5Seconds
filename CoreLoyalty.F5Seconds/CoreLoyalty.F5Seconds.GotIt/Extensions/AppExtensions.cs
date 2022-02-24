using Microsoft.AspNetCore.Builder;
using Ocelot.Middleware;

namespace CoreLoyalty.F5Seconds.GotIt.Extensions
{
    public static class AppExtensions
    {
        public static void UseOcelotExtension(this IApplicationBuilder app)
        {
            app.UseOcelot().Wait();
        }

        public static void UseSwaggerExtension(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GotItGateway");
            });
        }
    }
}
