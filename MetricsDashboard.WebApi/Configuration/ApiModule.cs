using Autofac;
using MetricsDashboard.Core.Tools;
using MetricsDashboard.WebApi.Services;

namespace MetricsDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Settings>().AsImplementedInterfaces();
            builder.RegisterType<DateTimeOffsetProvider>().AsImplementedInterfaces();
            builder.RegisterType<MetricService>().AsImplementedInterfaces();
            builder.RegisterType<MetricReactiveService>().AsImplementedInterfaces();
        }
    }
}
