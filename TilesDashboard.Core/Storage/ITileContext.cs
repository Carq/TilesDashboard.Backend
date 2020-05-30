using MongoDB.Driver;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Storage
{
    public interface ITileContext
    {
        IMongoCollection<TileDbEntity> GetTiles();
    }
}
