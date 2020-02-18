using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Core.Entities;
using MetricsDashboard.Core.Models;

namespace MetricsDashboard.Core
{
    public interface ITileRepository
    {
        Task<TileEntity> GetTileAsync(string tileName, CancellationToken cancellationToken);

        Task<IList<ITile>> GetMetricTilesAsync(CancellationToken cancellationToken);

        Task<IList<ITile>> GetBooleanTilesAsync(CancellationToken cancellationToken);

        Task SaveTileDataAsync<TValue>(MetricEntity<TValue> metric, CancellationToken cancellationToken);
    }
}