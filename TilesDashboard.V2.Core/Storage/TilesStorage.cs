using MongoDB.Driver;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Storage
{
    public class TilesStorage : ITilesStorage
    {
        private readonly IMongoDatabase _database;

        public TilesStorage(IMongoDatabase database)
        {
            _database = database;
        }

        public IMongoCollection<TileEntity> TilesInformation => _database.GetCollection<TileEntity>(CollectionNames.TilesInformation);

        public IMongoCollection<TileDataContainer> TilesData => _database.GetCollection<TileDataContainer>(CollectionNames.TilesData);
    }
}
