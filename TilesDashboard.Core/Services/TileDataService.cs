using System.Threading;
using System.Threading.Tasks;
using TilesDashboard.Contract;
using TilesDashboard.Contract.Enums;
using TilesDashboard.Core.Entities;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Extensions;
using TilesDashboard.Core.Repositories;
using TilesDashboard.Core.Tools;

namespace TilesDashboard.Core.Services
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

        public async Task SaveStatusAsync(SaveValueDto<bool> saveValueDto, CancellationToken cancellationToken)
        {
            await SaveTileDataAsync(saveValueDto, TileType.Status, cancellationToken);
        }

        private async Task SaveTileDataAsync<TValue>(
            SaveValueDto<TValue> saveValueDto,
            TileType tileType,
            CancellationToken cancellationToken)
        {
            var tile = await _tileRepository.GetTileAsync(saveValueDto.TileName, cancellationToken);
            if (tile.NotExists())
            {
                throw new NotFoundException($"Tile {saveValueDto.TileName} does not exist.");
            }

            if (tile.TileType != tileType)
            {
                throw new InvalidTileTypeException(tile.Name, tile.TileType, tileType);
            }

            var entity = new TileDataEntity<TValue>
            {
                TileId = tile.Id,
                Value = saveValueDto.Value,
                AddedOn = _dateTimeOffsetProvider.Now,
            };

            await _tileRepository.SaveTileDataAsync(entity, cancellationToken);
        }
    }
}
