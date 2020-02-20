using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Core.Mappers;

namespace MetricsDashboard.Core
{
    public class TileService : ITileService
    {
        private readonly ITileRepository _tileRepository;
        private readonly ITileMapper _tileMapper;

        public TileService(ITileRepository tileRepository, ITileMapper tileMapper)
        {
            _tileRepository = tileRepository;
            _tileMapper = tileMapper;
        }

        public async Task<IList<TileDto>> GetAllTilesAsync(CancellationToken cancellationToken)
        {
            var tiles = await Task.WhenAll(
                GetTilesAsync(_tileRepository.GetMetricTilesAsync, _tileMapper.ConvertMetricTile, cancellationToken),
                GetTilesAsync(_tileRepository.GetStatusTilesAsync, _tileMapper.ConvertStatusTile, cancellationToken));

            return tiles.SelectMany(t => t).ToList();
        }

        private static async Task<IEnumerable<TileDto>> GetTilesAsync<TEntity>(
            Func<CancellationToken, Task<IList<TEntity>>> loader,
            Func<TEntity, TileDto> mapper,
            CancellationToken cancellationToken)
        {
            var entities = await loader(cancellationToken);
            return entities.Select(mapper);
        }
    }
}
