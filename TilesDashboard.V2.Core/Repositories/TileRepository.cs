using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        public async Task CheckIfExist(TileId tileId)
        {
            var filter = TileMongoEntityExtensions.TileMongoEntityFilter(tileId);
            var exists = await _tileStorage.Tiles
                                          .Find(filter)
                                          .AnyAsync(_cancellationTokenProvider.GetToken());

            if (!exists)
            {
                throw new NotFoundException($"Tile {tileId} does not exist.");
            }
        }

        public async Task<IList<TileEntity>> GetAll()
        {
            return await _tileStorage.Tiles.Find(_ => true).ToListAsync(_cancellationTokenProvider.GetToken());
        }

        public async Task<TEntity> GetTile<TEntity>(TileId tileId)
            where TEntity : TileEntity
        {
            var filter = TileMongoEntityExtensions.TileMongoEntityFilter(tileId);
            var tileMongo = await _tileStorage.Tiles
                                          .Find(filter)
                                          .As<TEntity>()
                                          .SingleOrDefaultAsync(_cancellationTokenProvider.GetToken());

            if (tileMongo.NotExists())
            {
                throw new NotFoundException($"Tile {tileId} does not exist.");
            }

            return tileMongo;
        }

        public void RecordValue(TileId tileId, ITileValue tileValue)
        {
            throw new System.NotImplementedException();
        }
    }
}
