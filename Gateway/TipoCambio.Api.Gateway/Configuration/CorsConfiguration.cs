using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace TipoCambio.Api.Gateway.Configuration
{
    public static class CorsConfiguration
    {
        public static IApplicationBuilder UseCorsConfig(this IApplicationBuilder app, string[] Cors)
        {
            using (var provider = app.ApplicationServices.CreateScope())
            {
                app.UseCors(config =>
                {
                    config.WithOrigins(Cors)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
                });
            }

            return app;
        }
    }
}