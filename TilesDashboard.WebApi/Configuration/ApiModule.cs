using Autofac;
using Microsoft.Extensions.Hosting;
using TilesDashboard.Handy.Tools;
using TilesDashboard.WebApi.BackgroundWorkers;

namespace TilesDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileDashboardSettings>().AsImplementedInterfaces();
            builder.RegisterType<DateTimeOffsetProvider>().AsImplementedInterfaces();
        }
    }
}
