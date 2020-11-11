using MongoDB.Bson;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.V2.Core.Storage
{
    public class TileMongoEntity
    {
        public string Id { get; private set; }

        public TileId TileId { get; private set; }

        public BsonDocument TileConfiguration { get; private set; }
    }
}
