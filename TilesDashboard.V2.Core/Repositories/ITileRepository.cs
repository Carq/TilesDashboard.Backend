using System.Collections.Generic;
using System.Threading.Tasks;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Repositories
{
    public interface ITileRepository
    {
        Task CheckIfExist(TileId tileId);

        Task<IList<TileEntity>> GetAll();

        Task<TEntity> GetTile<TEntity>(TileId tileId) 
            where TEntity : TileEntity;

        void RecordValue(TileId tileId, ITileValue tileValue);
    }
}