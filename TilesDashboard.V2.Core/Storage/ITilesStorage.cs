using MongoDB.Driver;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Storage
{
    public interface ITilesStorage
    {
        IMongoCollection<TileEntity> Tiles { get; }
    }
}
