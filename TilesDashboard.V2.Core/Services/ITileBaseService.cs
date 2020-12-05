using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Services
{
    public interface ITileBaseService
    {
        Task<IList<TileEntity>> GetAllTiles();

        Task<TTile> GetTile<TTile>(TileId tileId)
            where TTile : TileEntity;

        Task<TileEntity> GetTile(TileId tileId);
    }
}