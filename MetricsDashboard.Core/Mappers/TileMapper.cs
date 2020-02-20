using MetricsDashboard.Contract;
using MetricsDashboard.Contract.Enums;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Mappers
{
    public class TileMapper : ITileMapper
    {
        public TileDto ConvertStatusTile(TileEntity tile, MetricEntity<bool> status)
        {
            return new TileDto
            {
                Name = tile.Name,
                TileType = TileType.Status,
                TileData = new StatusDto
                {
                    Current = status.Value,
                    LastUpdated = status.AddedOn,
                },
            };
        }

        public TileDto ConvertMetricTile(
            TileEntity tile,
            MetricEntity<decimal> metric,
            MetricSettingsEntity metricSettings)
        {
            return new TileDto
            {
                Name = tile.Name,
                TileType = TileType.Metric,
                TileData = new MetricDto
                {
                    Goal = metricSettings.Goal,
                    Limit = metricSettings.Limit,
                    Wish = metricSettings.Wish,
                    Type = metricSettings.MetricType,
                    Current = metric.Value,
                    LastUpdated = metric.AddedOn,
                },
            };
        }
    }
}
