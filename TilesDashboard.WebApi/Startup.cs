using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TilesDashboard.PluginSystem.Configuration;
using TilesDashboard.V2.Core.Configuration;
using TilesDashboard.WebApi.BackgroundWorkers;
using TilesDashboard.WebApi.Configuration;
using TilesDashboard.WebApi.Hubs;
using TilesDashboard.WebApi.Middlewares;
using TilesDashboard.WebApi.StartupConfig;

namespace TilesDashboard.WebApi
{
    [SuppressMessage("Microsoft.Performance", "CA1822", Justification = "Required from ASP .NET Core")]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(Configuration).CreateLogger();

            services.AddSingleton<IHostedService, PluginBackgroundWorker>();

            services.AddCors(Configuration);
            services.AddControllers().AddJsonOptions(jsonOption =>
                                                     {
                                                         jsonOption.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                                                         jsonOption.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                                                     });
            services.AddSignalR();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ApiModule>();
            builder.RegisterModule<CoreV2Module>();
            builder.RegisterModule<PluginSystemModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
            app.UseRouting();
            app.UseCors(Configuration);
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<TilesNotificationHub>("/notifications");
            });

            app.LoadNotificationPlugins();
            app.LoadDataPlugins();
        }
    }
}
