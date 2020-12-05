using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Extensions;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.Handy.Tools;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Exceptions;
using TilesDashboard.V2.Core.Storage;

namespace TilesDashboard.V2.Core.Repositories
{
    public class TileRepository : ITileRepository
    {
        private readonly ITilesStorage _tileStorage;

        private readonly ICancellationTokenProvider _cancellationTokenProvider;

        public TileRepository(ITilesStorage tileStorage, ICancellationTokenProvider cancellationTokenProvider)
        {
            _tileStorage = tileStorage;
            _cancellationTokenProvider = cancellationTokenProvider;
        }

        public async Task<TileStorageId> CheckIfExist(TileId tileId)
        {
            var filter = TileMongoEntityExtensions.TileMongoEntityFilter(tileId);
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

        public async Task<TEntity> GetTile<TEntity>(TileId tileId)
            where TEntity : TileEntity
        {
            var filter = TileMongoEntityExtensions.TileMongoEntityFilter(tileId);
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

            var filter = Builders<TileData>.Filter.And(
                Builders<TileData>.Filter.Eq(x => x.TileStorageId, tileStorageId.Value),
                Builders<TileData>.Filter.Eq(x => x.GroupDate, TileData.GenerateGroupDate(tileValue.AddedOn)),
                Builders<TileData>.Filter.Eq(x => x.Type, tileId.Type));

            await _tileStorage.TilesData.UpdateOneAsync(
                filter,
                Builders<TileData>.Update.Push(x => x.Data, tileValue),
                new UpdateOptions() { IsUpsert = true },
                _cancellationTokenProvider.GetToken());
        }
    }
}
