using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Repositories
{
    public interface ITileRepository
    {
        Task<TileEntity> GetTileAsync(string tileName, CancellationToken cancellationToken);

        Task<IList<(TileEntity, TileDataEntity<decimal>, MetricSettingsEntity)>> GetMetricTilesAsync(CancellationToken cancellationToken);

        Task<IList<(TileEntity, TileDataEntity<bool>)>> GetStatusTilesAsync(CancellationToken cancellationToken);

        Task SaveTileDataAsync<TValue>(TileDataEntity<TValue> metric, CancellationToken cancellationToken);
    }
}