using Autofac;
using MetricsDashboard.Core;

namespace MetricsDashboard.WebApi.Configuration
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TileService>().As<ITileService>();
            builder.RegisterType<TileRepository>().As<ITileRepository>();
        }
    }
}
