using MongoDB.Driver;
using TilesDashboard.Core.Entities;

namespace TilesDashboard.Core.Storage
{
    public interface ITileContext
    {
        IMongoCollection<TileDbEntity<TData>> GetTiles<TData>()
            where TData : TileData;
    }
}
