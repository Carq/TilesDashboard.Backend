using Autofac;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.WebApi.BackgroundWorkers;
using TilesDashboard.WebApi.Hubs;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Loaders;
using TilesDashboard.WebApi.PluginSystem.Notifications;

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

        private static void PluginInfrastructure(ContainerBuilder builder)
        {
            builder.RegisterType<PluginSystemConfig>().As<IPluginSystemConfig>();
            builder.RegisterType<PluginConfigProvider>().As<IPluginConfigProvider>();
            RegisterNotificationPluginStuff(builder);
            RegisterDataPluginStuff(builder);
        }

        private static void RegisterNotificationPluginStuff(ContainerBuilder builder)
        {
            builder.RegisterType<NotificationPluginLoader>().As<INotificationPluginLoader>();
            builder.RegisterType<NotificationPluginRepository>().As<INotificationPluginRepository>();
            builder.RegisterType<NotificationPluginContext>().As<INotificationPluginContext>().SingleInstance();
            builder.RegisterType<PluginNotificationEventHandler>().As<IEventHandler<NewDataEvent>>().InstancePerLifetimeScope();
        }

        private static void RegisterDataPluginStuff(ContainerBuilder builder)
        {
            builder.RegisterType<DataPluginLoader>().As<IDataPluginLoader>();
            builder.RegisterType<WeatherPluginHandler>();
            builder.RegisterType<MetricPluginHandler>();
            builder.RegisterType<IntegerPluginHandler>();
            builder.RegisterType<HeartBeatPluginHandler>();
        }
    }
}
