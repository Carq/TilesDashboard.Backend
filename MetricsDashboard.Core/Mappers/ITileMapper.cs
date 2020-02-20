using MetricsDashboard.Contract;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Mappers
{
    public interface ITileMapper
    {
        TileDto ConvertStatusTile((TileEntity, MetricEntity<bool>) tileData)
            => ConvertStatusTile(tileData.Item1, tileData.Item2);

        TileDto ConvertStatusTile(TileEntity tile, MetricEntity<bool> status);

        TileDto ConvertMetricTile((TileEntity, MetricEntity<decimal>, MetricSettingsEntity) tileData)
            => ConvertMetricTile(tileData.Item1, tileData.Item2, tileData.Item3);

        TileDto ConvertMetricTile(
            TileEntity tile,
            MetricEntity<decimal> metric,
            MetricSettingsEntity metricSettings);
    }
}