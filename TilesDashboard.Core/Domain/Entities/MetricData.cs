using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using TilesDashboard.Core.Domain.ValueObjects;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Core.Type;

namespace TilesDashboard.Core.Domain.Entities
{
    public class MetricData : TileData
    {
        public MetricData(decimal value, MetricType type,  DateTimeOffset addedOn) : base(addedOn)
        {
          switch (type)
          {
                case MetricType.Percentage:
                    Percentage.Validate(value);
                    break;
                default:
                    throw new NotSupportedException($"Metric Type {type} is not supported");
          }

          Value = value;
        }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; private set; }
    }
}
