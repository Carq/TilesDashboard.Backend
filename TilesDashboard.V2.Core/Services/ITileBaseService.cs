using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface ITileBaseService
    {
        Task<IList<TileEntity>> GetAllTiles();

        Task<IList<TileEntityWithData>> GetAllTilesWithRecentData();

        Task<TileEntity> GetTile(TileId tileId);

        Task<IList<TileValue>> GetTileRecentData(TileId tileId, int amountOfRecentData);

        Task<IList<TileValue>> GetTileSinceData(TileId tileId, int hours);
    }
}