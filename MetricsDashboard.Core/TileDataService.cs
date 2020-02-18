using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;
using MetricsDashboard.Contract.Enums;
using MetricsDashboard.Core.Entities;
using MetricsDashboard.Core.Exceptions;
using MetricsDashboard.Core.Tools;

namespace MetricsDashboard.Core
{
    public class TileDataService : ITileDataService
    {
        private readonly ITileRepository _tileRepository;
        private readonly IDateTimeOffsetProvider _dateTimeOffsetProvider;

        public TileDataService(ITileRepository tileRepository, IDateTimeOffsetProvider dateTimeOffsetProvider)
        {
            _tileRepository = tileRepository;
            _dateTimeOffsetProvider = dateTimeOffsetProvider;
        }

        public async Task SaveMetricAsync(SaveValueDto<decimal> saveValueDto, CancellationToken cancellationToken)
        {
            await SaveTileDataAsync(saveValueDto, TileType.Metric, cancellationToken);
        }

        public async Task SaveBooleanAsync(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken)
        {
            await SaveTileDataAsync(saveValueDto, TileType.Boolean, cancellationToken);
        }

        private async Task SaveTileDataAsync<TValue>(
            SaveValueDto<TValue> saveValueDto,
            TileType tileType,
            CancellationToken cancellationToken)
        {
            var tile = await _tileRepository.GetTileAsync(saveValueDto.Name, cancellationToken);
            if (tile == null)
            {
                throw new NotFoundException($"Tile {saveValueDto.Name} does not exist.");
            }

            if (tile.TileType != tileType)
            {
                throw new InvalidTileTypeException(tile.Name, tile.TileType, tileType);
            }

            var entity = new MetricEntity<TValue>
            {
                TileId = tile.Id,
                Value = saveValueDto.Value,
                AddedOn = _dateTimeOffsetProvider.Now,
            };

            await _tileRepository.SaveTileDataAsync(entity, cancellationToken);
        }
    }
}
