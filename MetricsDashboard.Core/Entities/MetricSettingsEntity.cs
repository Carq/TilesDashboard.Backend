using MetricsDashboard.Contract.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.Core.Entities
{
    public class MetricSettingsEntity
    {
        public ObjectId Id { get; set; }

        public ObjectId TileId { get; set; }

        public decimal Limit { get; set; }

        public decimal? Wish { get; set; }

        public decimal? Goal { get; set; }

        [BsonRepresentation(BsonType.String)]
        public MetricType MetricType { get; set; }
    }
}