using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.DataAccess.Entities
{
    public class Metric<TValue> : Metric
    {
        [BsonElement("value")]
        public TValue Value { get; set; }

        public override object MetricValue => Value;
    }
}
