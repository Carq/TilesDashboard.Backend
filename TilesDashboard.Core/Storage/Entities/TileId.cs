using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Core.Type.Enums;

namespace TilesDashboard.Core.Storage.Entities
{
    public class TileId
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TileType TileType { get; set; }
    }
}
