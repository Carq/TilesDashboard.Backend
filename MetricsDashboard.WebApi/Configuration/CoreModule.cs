using Autofac;
using MetricsDashboard.Core.Mappers;
using MetricsDashboard.Core.Repositories;
using MetricsDashboard.Core.Services;

namespace MetricsDashboard.WebApi.Configuration
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileService>().As<ITileService>();
            builder.RegisterType<TileDataService>().As<ITileDataService>();
            builder.RegisterType<TileRepository>().As<ITileRepository>();
            builder.RegisterType<TileMapper>().As<ITileMapper>();
        }
    }
}
