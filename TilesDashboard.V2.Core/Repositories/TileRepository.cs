using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Domain.Extensions;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
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

        public async Task<StorageId> CheckIfExist(TileId tileId)
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

            return new StorageId(storageId);
        }

        public async Task<TileId> CheckIfExist(StorageId id, TileType tileType)
        {
            var filter = TileEntityExtensions.TileEntityFilter(id, tileType);
            var projection = Builders<TileEntity>.Projection.Expression<TileId>(x => x.TileId);

            var tileId = await _tileStorage.TilesInformation
                                          .Find(filter)
                                          .Project(projection)
                                          .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken());

            if (tileId.NotExists())
            {
                throw new NotFoundException($"Tile {tileType} with Id {id.Value} does not exist.");
            }

            return tileId;
        }

        public async Task<IList<TileEntity>> GetAll()
        {
            return await _tileStorage.TilesInformation.Find(_ => true).ToListAsync(_cancellationTokenProvider.GetToken());
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

        public async Task<TEntity> GetTile<TEntity>(StorageId storageId, TileType tileType)
            where TEntity : TileEntity
        {
            var filter = TileEntityExtensions.TileEntityFilter(storageId, tileType);
            var tileMongo = await _tileStorage.TilesInformation
                                          .Find(filter)
                                          .As<TEntity>()
                                          .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken());

            if (tileMongo.NotExists())
            {
                throw new NotFoundException($"Tile {tileType} with Id {storageId} does not exist.");
            }

            return tileMongo;
        }

        public async Task<IList<TileValue>> GetRecentTileValues(TileId tileId, int amountOfRecentData)
        {
            var tileStorageId = await CheckIfExist(tileId);

            var filter = TileDataContainerExtensions.TileEntityFilter(tileStorageId, tileId.Type);
            var group = BsonSerializer.Deserialize<BsonDocument>($"{{ _id: \"${nameof(TileDataContainer.TileStorageId)}\", Data: {{ $push: \"$Data\" }} }}");
            var project = BsonSerializer.Deserialize<BsonDocument>($"{{ Data: {{ $slice: [\"$Data\", {-amountOfRecentData}] }} }}");

            return (await _tileStorage
                    .TilesData
                    .Aggregate()
                    .Match(filter)
                    .Unwind(x => x.Data)
                    .Group(group)
                    .Project<TileDataContainer>(project)
                    .ToListAsync(_cancellationTokenProvider.GetToken()))
                    ?.SelectMany(x => x.Data)
                    .OrderByDescending(x => x.AddedOn)
                    .ToList();
        }

        public async Task<IList<TileValue>> GetTileValuesSince(TileId tileId, DateTimeOffset dateTimeSince)
        {
            var tileStorageId = await CheckIfExist(tileId);

            var filter = TileDataContainerExtensions.TileEntityFilter(tileStorageId, tileId.Type);
            var onlyToday = Builders<BsonDocument>.Filter.Gte($"{nameof(TileDataContainer.Data)}.{nameof(TileValue.AddedOn)}", dateTimeSince.UtcDateTime.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture));
            var group = BsonSerializer.Deserialize<BsonDocument>($"{{ _id: \"${nameof(TileDataContainer.TileStorageId)}\", Data: {{ $push: \"$Data\" }} }}");

            return (await _tileStorage
                    .TilesData
                    .Aggregate()
                    .Match(filter)
                    .Unwind(x => x.Data)
                    .Match(onlyToday)
                    .Group(group)
                    .As<TileDataContainer>()
                    .ToListAsync(_cancellationTokenProvider.GetToken()))
                    ?.SelectMany(x => x.Data)
                    .ToList();
        }

        public async Task RecordValue(TileId tileId, TileValue tileValue)
        {
            var tileStorageId = await CheckIfExist(tileId);
            await RecordValueById(tileStorageId, tileValue, tileId);
        }

        public async Task RecordValue(StorageId id, TileValue tileValue, TileType tileType)
        {
            var tileId = await CheckIfExist(id, tileType);
            await RecordValueById(id, tileValue, tileId);
        }

        private async Task RecordValueById(StorageId tileStorageId, TileValue tileValue, TileId tileId)
        {
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

            await _eventDispatcher.PublishAsync(new NewDataEvent(tileId, tileStorageId, tileValue), cancellationToken);
        }
    }
}
