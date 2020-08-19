using Autofac;
using TilesDashboard.Core.Domain.Repositories;
using TilesDashboard.Core.Domain.Services;
using TilesDashboard.Core.Storage;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.Core.Configuration
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileContext>().As<ITileContext>();
            builder.RegisterType<TilesRepository>().As<ITilesRepository>();
            builder.RegisterType<TileService>().As<ITileService>();

            builder.RegisterType<WeatherRepository>().As<IWeatherRepository>();
            builder.RegisterType<WeatherService>().As<IWeatherServices>();
            builder.RegisterType<MetricService>().As<IMetricService>();
            builder.RegisterType<IntegerTileService>().As<IIntegerTileService>();
            builder.RegisterType<HeartBeatService>().As<IHeartBeatService>();
            builder.RegisterType<EventDispatcher>().As<IEventDispatcher>();
        }
    }
}
