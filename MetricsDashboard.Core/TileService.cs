using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;

namespace MetricsDashboard.Core
{
    public class TileService : ITileService
    {
        private readonly ITileRepository _tileRepository;

        public TileService(ITileRepository tileRepository)
        {
            _tileRepository = tileRepository;
        }

        public async Task<IList<TileDto>> GetAllTilesAsync(CancellationToken cancellationToken)
        {
            var tiles = await Task.WhenAll(
                _tileRepository.GetMetricTilesAsync(cancellationToken),
                _tileRepository.GetBooleanTilesAsync(cancellationToken));

            return tiles.SelectMany(t => t).Select(t => t.ToDto()).ToList();
        }
    }
}
