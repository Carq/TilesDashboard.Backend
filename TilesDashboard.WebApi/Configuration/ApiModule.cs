using Autofac;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.WebApi.Hubs;
using TilesDashboard.WebApi.PluginSystem;

namespace TilesDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileDashboardSettings>().As<IDatabaseConfiguration>().As<ISecurityConfig>().SingleInstance();
            builder.RegisterType<DateTimeOffsetProvider>().As<IDateTimeOffsetProvider>().SingleInstance();
            builder.RegisterType<TilesNotificationHub>().As<IEventHandler<NewDataEvent>>().InstancePerLifetimeScope();

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
