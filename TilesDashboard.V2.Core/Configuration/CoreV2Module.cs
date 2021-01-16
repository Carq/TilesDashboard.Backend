using Autofac;
using TilesDashboard.Handy.Events;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;
using TilesDashboard.V2.Core.Storage;

namespace TilesDashboard.V2.Core.Configuration
{
    public class CoreV2Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            BsonMapping.RegisterMappings();
            builder.RegisterType<TilesStorage>().As<ITilesStorage>().InstancePerLifetimeScope();
            builder.RegisterType<TileRepository>().As<ITileRepository>().InstancePerLifetimeScope();
            builder.RegisterType<MetricService>().As<IMetricService>().InstancePerLifetimeScope();
            builder.RegisterType<WeatherService>().As<IWeatherService>().InstancePerLifetimeScope();
            builder.RegisterType<IntegerService>().As<IIntegerService>().InstancePerLifetimeScope();
            builder.RegisterType<TileBaseService>().As<ITileBaseService>().InstancePerLifetimeScope();
            builder.RegisterType<HeartBeatService>().As<IHeartBeatService>().InstancePerLifetimeScope();

            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
        }
    }
}
