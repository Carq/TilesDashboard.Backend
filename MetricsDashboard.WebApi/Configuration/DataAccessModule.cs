using Autofac;
using MetricsDashboard.DataAccess;
using MetricsDashboard.DataAccess.Interfaces;

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
