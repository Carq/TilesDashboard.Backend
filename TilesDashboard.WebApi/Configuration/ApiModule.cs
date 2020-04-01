using Autofac;
using TilesDashboard.Core.Configuration;
using TilesDashboard.Handy.Tools;
using TilesDashboard.WebApi.PluginInfrastructure;

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
            builder.RegisterType<PluginLoader>().AsImplementedInterfaces();
        }
    }
}
