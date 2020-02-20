using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Repositories
{
    public interface ITileRepository
    {
        Task<TileEntity> GetTileAsync(string tileName, CancellationToken cancellationToken);

        Task<IList<(TileEntity, MetricEntity<decimal>, MetricSettingsEntity)>> GetMetricTilesAsync(CancellationToken cancellationToken);

        Task<IList<(TileEntity, MetricEntity<bool>)>> GetStatusTilesAsync(CancellationToken cancellationToken);

        Task SaveTileDataAsync<TValue>(MetricEntity<TValue> metric, CancellationToken cancellationToken);
    }
}