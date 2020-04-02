using Autofac;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.WebApi.PluginSystem;

namespace TilesDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileDashboardSettings>().As<IDatabaseConfiguration>();
            builder.RegisterType<DateTimeOffsetProvider>().As<IDateTimeOffsetProvider>();

            PluginInfrastructure(builder);
        }

        private void PluginInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterType<PluginLoader>().As<IPluginLoader>();
            builder.RegisterType<PluginSystemConfig>().As<IPluginSystemConfig>();
            builder.RegisterType<PluginConfigProvider>().As<IPluginConfigProvider>();
        }
    }
}
