using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.Core.Storage.Entities
{
    public class TileId
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TileType TileType { get; set; }
    }
}
