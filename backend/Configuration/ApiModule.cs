using Autofac;
using MetricsDashboard.WebApi.Services;

namespace MetricsDashboard.WebApi.Configuration
{
    public class ApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MetricService>().AsImplementedInterfaces();
            builder.RegisterType<Settings>().AsImplementedInterfaces();
        }
    }
}
