using Autofac;
using TilesDashboard.Core.Tools;

namespace TilesDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Settings>().AsImplementedInterfaces();
            builder.RegisterType<DateTimeOffsetProvider>().AsImplementedInterfaces();
        }
    }
}
