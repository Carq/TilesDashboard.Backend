using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Core.Storage.Entities
{
    public class TileId
    {
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TileType TileType { get; set; }
    }
}
