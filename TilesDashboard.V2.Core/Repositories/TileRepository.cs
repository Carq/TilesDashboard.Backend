using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Domain.Extensions;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Exceptions;
using TilesDashboard.V2.Core.Storage;
using TilesDashboard.V2.Core.Storage.Extensions;

namespace TilesDashboard.V2.Core.Repositories
{
    public class TileRepository : ITileRepository
    {
        private readonly ITilesStorage _tileStorage;

        private readonly IEventDispatcher _eventDispatcher;

        private readonly ICancellationTokenProvider _cancellationTokenProvider;

        public TileRepository(ITilesStorage tileStorage, ICancellationTokenProvider cancellationTokenProvider, IEventDispatcher eventDispatcher)
        {
            _tileStorage = tileStorage;
            _cancellationTokenProvider = cancellationTokenProvider;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<TileStorageId> CheckIfExist(TileId tileId)
        {
            var filter = TileEntityExtensions.TileEntityFilter(tileId);
            var projection = Builders<TileEntity>.Projection.Expression<string>(x => x.Id);

            var storageId = await _tileStorage.TilesInformation
                                          .Find(filter)
                                          .Project(projection)
                                          .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken());

            if (string.IsNullOrWhiteSpace(storageId))
            {
                throw new NotFoundException($"Tile {tileId} does not exist.");
            }

            return new TileStorageId(storageId);
        }

        public async Task<IList<TileEntity>> GetAll()
        {
            return await _tileStorage.TilesInformation.Find(_ => true).ToListAsync(_cancellationTokenProvider.GetToken());
        }

        public async Task<IList<TileValue>> GetRecentData(TileId tileId, int amountOfRecentData)
        {
            var tileStorageId = await CheckIfExist(tileId);

            var filter = TileDataContainerExtensions.TileEntityFilter(tileStorageId, tileId.Type);
            var projection = Builders<TileDataContainer>.Projection.FetchRecentData(amountOfRecentData);

            return (await _tileStorage
                    .TilesData
                    .Find(filter)
                    .Project<TileDataContainer>(projection)
                    .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken()))?.Data;
        }

        public async Task<TEntity> GetTile<TEntity>(TileId tileId)
            where TEntity : TileEntity
        {
            var filter = TileEntityExtensions.TileEntityFilter(tileId);
            var tileMongo = await _tileStorage.TilesInformation
                                          .Find(filter)
                                          .As<TEntity>()
                                          .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken());

            if (tileMongo.NotExists())
            {
                throw new NotFoundException($"Tile {tileId} does not exist.");
            }

            return tileMongo;
        }

        public async Task RecordValue(TileId tileId, TileValue tileValue)
        {
            var tileStorageId = await CheckIfExist(tileId);
            var cancellationToken = _cancellationTokenProvider.GetToken();

            var filter = Builders<TileDataContainer>.Filter.And(
                Builders<TileDataContainer>.Filter.Eq(x => x.TileStorageId, tileStorageId.Value),
                Builders<TileDataContainer>.Filter.Eq(x => x.GroupDate, TileDataContainer.GenerateGroupDate(tileValue.AddedOn)),
                Builders<TileDataContainer>.Filter.Eq(x => x.Type, tileId.Type));

            await _tileStorage.TilesData.UpdateOneAsync(
                filter,
                Builders<TileDataContainer>.Update.Push(x => x.Data, tileValue),
                new UpdateOptions() { IsUpsert = true, },
                cancellationToken);

            await _eventDispatcher.PublishAsync(new NewDataEvent(tileId, tileValue), cancellationToken);
        }
    }
}
