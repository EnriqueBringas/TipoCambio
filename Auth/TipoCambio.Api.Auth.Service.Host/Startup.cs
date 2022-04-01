using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TipoCambio.Api.Auth.Entity.Entities.Models;
using TipoCambio.Api.Auth.Interface.Interfaces.v1;
using TipoCambio.Api.Auth.Service.Services.v1;

namespace TipoCambio.Api.Auth.Service.Host
{
    public class Startup
    {
        public IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddApiVersioning();

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway - Authentication", Version = "v1" }));

            services.AddScoped(config => Config.GetSection("ServiceSettings").Get<ServiceConfig>());
            services.AddScoped<ITbUsersJwtService, TbUsersJwtService>();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "API version 1.0"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
