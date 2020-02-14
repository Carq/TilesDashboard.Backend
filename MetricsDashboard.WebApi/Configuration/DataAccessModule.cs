using Autofac;
using MetricsDashboard.Core;
using MetricsDashboard.Core.Interfaces;

namespace MetricsDashboard.WebApi.Configuration
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MetricsRepository>().As<IMetricsRepository>();
        }
    }
}
