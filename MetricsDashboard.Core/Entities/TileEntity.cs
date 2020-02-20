using MetricsDashboard.Contract.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.Core.Entities
{
    public class TileEntity
    {
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public TileType TileType { get; set; }
    }
}
