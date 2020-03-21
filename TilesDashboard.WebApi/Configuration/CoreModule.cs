using Autofac;
using TilesDashboard.Core.Mappers;
using TilesDashboard.Core.Repositories;
using TilesDashboard.Core.Services;

namespace TilesDashboard.WebApi.Configuration
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
