using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public abstract class DecimalValueObject : ValueObject
    {
        protected DecimalValueObject(decimal value)
        {
            Value = value;
        }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
