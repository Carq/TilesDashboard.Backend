using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.DataAccess.Entities
{
    public class MetricType
    {
        [BsonElement("name")]
        public string MetricName { get; set; }

        [BsonElement("kind")]
        public string MetricKind { get; set; }
    }
}