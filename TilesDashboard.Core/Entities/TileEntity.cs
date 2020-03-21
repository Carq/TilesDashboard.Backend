using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Core.Entities
{
    public class TileEntity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TileType TileType { get; set; }
    }
}
