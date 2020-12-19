using Autofac;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using TilesDashboard.Contract.Events;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Notification;
using TilesDashboard.V2.Core.Configuration;
using TilesDashboard.WebApi.BackgroundWorkers;
using TilesDashboard.WebApi.Hubs;
using TilesDashboard.WebApi.Middlewares;
using TilesDashboard.WebApi.PluginSystem;
using TilesDashboard.WebApi.PluginSystem.Loaders;
using TilesDashboard.WebApi.PluginSystem.Notifications;

namespace TilesDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GlobalExceptionHandlingMiddleware>();
            builder.RegisterType<HttpContextAccessor>().As<IHttpContextAccessor>().SingleInstance();
            builder.RegisterType<CancellationTokenProvider>().As<ICancellationTokenProvider>().InstancePerLifetimeScope();
            builder.RegisterType<TileDashboardSettings>().As<IDatabaseConfiguration>().As<ISecurityConfig>().SingleInstance();
            builder.RegisterType<DateTimeOffsetProvider>().As<IDateTimeProvider>().SingleInstance();
            builder.RegisterType<TilesNotificationHub>().As<IEventHandler<NewDataEvent>>().InstancePerLifetimeScope();
            builder.RegisterType<TilesNotificationHub>().As<IEventHandler<NewDataEvent>>().InstancePerLifetimeScope();
            RegisterDatabase(builder);
            PluginInfrastructure(builder);
        }

        private static void RegisterDatabase(ContainerBuilder builder)
        {
            builder.Register((c, p) =>
            {
                var config = c.Resolve<IDatabaseConfiguration>();
                var client = new MongoClient(config.ConnectionString);
                return client.GetDatabase(config.DatabaseName);
            }).As<IMongoDatabase>()
            .InstancePerLifetimeScope();
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
