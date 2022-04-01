using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TipoCambio.Api.Gateway.Configuration;
using TipoCambio.Api.Gateway.Interfaces.v1;
using TipoCambio.Api.Gateway.Models;
using TipoCambio.Api.Gateway.Services.v1;

namespace TipoCambio.Api.Gateway
{
    public class Startup
    {
        public IConfiguration Config { get; }
        public ServiceConfig ServiceConfig { get; set; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            ServiceConfig = Config.GetSection("Endpoints").Get<ServiceConfig>();
            var lkey = Encoding.UTF8.GetBytes(Config.GetValue<string>("SecretKey"));

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(builder => {
                builder.RequireHttpsMetadata = false;
                builder.SaveToken = true;
                builder.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(lkey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            /*services.AddCors(o => o.AddPolicy("EnableCors", builder =>
            {
                builder.SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            }));*/

            services.AddApiVersioning();

            services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" }));

            services.AddScoped<IAuthenticationService>(c => new AuthenticationService(ServiceConfig.AuthenticationAPI));
            services.AddScoped<IBackendService>(c => new BackendService(ServiceConfig.BackendAPI));

            services.AddControllers();
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
            app.UseCorsConfig(ServiceConfig.Cors);
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}