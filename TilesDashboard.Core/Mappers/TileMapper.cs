using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Mappers
{
    public class TileMapper : ITileMapper
    {
        public TileDto ConvertStatusTile(TileEntity tile, TileDataEntity<bool> status)
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

        public TileDto ConvertMetricTile(TileEntity tile, TileDataEntity<decimal> metric, MetricSettingsEntity metricSettings)
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
