using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public class MetricTile : TileEntity
    {
        private MetricConfiguration _metricConfiguration;

        public MetricType MetricType => GetMetricConfiguration().MetricType;

        public override object GetConfigurationAsObject() => GetMetricConfiguration();

        public MetricConfiguration GetMetricConfiguration()
        {
            if (_metricConfiguration.NotExists())
            {
                _metricConfiguration = new MetricConfiguration(TileConfiguration);
            }

            return _metricConfiguration;
        }
    }
}
