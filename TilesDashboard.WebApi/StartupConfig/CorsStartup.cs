using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TilesDashboard.WebApi.Configuration;

namespace TilesDashboard.WebApi.StartupConfig
{
    public static class CorsStartup
    {
        public static void AddCors(this IServiceCollection services, IConfiguration configuration)
        {
            var corsConfig = new CorsConfig(configuration);
            if (string.IsNullOrWhiteSpace(corsConfig.Name))
            {
                return;
            }

            services.AddCors(
                options => options.AddPolicy(
                    corsConfig.Name,
                    corsBuilder =>
                    {
                        var builder = corsBuilder
                            .WithOrigins(corsConfig.AllowedOrigins)
                            .WithHeaders(corsConfig.AllowedHeaders)
                            .WithMethods(corsConfig.AllowedMethods)
                            .WithExposedHeaders(corsConfig.ExposedHeaders);

                        if (corsConfig.AllowCredentials)
                        {
                            builder.AllowCredentials();
                        }
                        else
                        {
                            builder.DisallowCredentials();
                        }

                        builder.Build();
                    }));
        }

        public static void UseCors(this IApplicationBuilder app, IConfiguration configuration)
        {
            var corsConfig = new CorsConfig(configuration);
            if (string.IsNullOrWhiteSpace(corsConfig.Name))
            {
                return;
            }

            app.UseCors(corsConfig.Name);
        }
    }
}
