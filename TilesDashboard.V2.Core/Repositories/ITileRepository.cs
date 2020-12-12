using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Storage;

namespace TilesDashboard.V2.Core.Repositories
{
    public interface ITileRepository
    {
        Task<TileStorageId> CheckIfExist(TileId tileId);

        Task<IList<TileEntity>> GetAll();

        Task<TEntity> GetTile<TEntity>(TileId tileId)
            where TEntity : TileEntity;

        Task RecordValue(TileId tileId, TileValue tileValue);

        Task<IList<TileValue>> GetRecentTileValues(TileId tileId, int amountOfRecentData);

        Task<IList<TileValue>> GetTileValuesSince(TileId tileId, DateTimeOffset dateTimeSince);
    }
}