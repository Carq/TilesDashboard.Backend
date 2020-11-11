using TilesDashboard.Handy.Extensions;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public class MetricTile : TileEntity
    {
        private MetricConfiguration _metricConfiguration;

        public MetricConfiguration Configuration => GetMetricConfiguration();

        public MetricType MetricType => Configuration.MetricType;

        private MetricConfiguration GetMetricConfiguration()
        {
            if (_metricConfiguration.NotExists())
            {
                _metricConfiguration = new MetricConfiguration(TileConfiguration);
            }

            return _metricConfiguration;
        }
    }
}
