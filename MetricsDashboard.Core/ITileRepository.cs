using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Core.Models;

namespace MetricsDashboard.Core
{
    public interface ITileRepository
    {
        Task<IList<ITile>> GetMetricTilesAsync(CancellationToken cancellationToken);

        Task<IList<ITile>> GetBooleanTilesAsync(CancellationToken cancellationToken);
    }
}