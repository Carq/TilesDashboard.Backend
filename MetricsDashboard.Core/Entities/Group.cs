using MongoDB.Bson.Serialization.Attributes;

namespace MetricsDashboard.Core.Entities
{
    public class Group<TKey>
    {
        [BsonId]
        public TKey Id { get; set; }
    }
}