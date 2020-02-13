using Autofac;
using MetricsDashboard.WebApi.Tools;

namespace MetricsDashboard.WebApi.Configuration
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
