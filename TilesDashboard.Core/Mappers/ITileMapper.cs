using TilesDashboard.Contract;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Mappers
{
    public interface ITileMapper
    {
        TileDto ConvertStatusTile((TileEntity, TileDataEntity<bool>) tileData)
            => ConvertStatusTile(tileData.Item1, tileData.Item2);

        TileDto ConvertStatusTile(TileEntity tile, TileDataEntity<bool> status);

        TileDto ConvertMetricTile((TileEntity, TileDataEntity<decimal>, MetricSettingsEntity) tileData)
            => ConvertMetricTile(tileData.Item1, tileData.Item2, tileData.Item3);

        TileDto ConvertMetricTile(TileEntity tile, TileDataEntity<decimal> metric, MetricSettingsEntity metricSettings);
    }
}