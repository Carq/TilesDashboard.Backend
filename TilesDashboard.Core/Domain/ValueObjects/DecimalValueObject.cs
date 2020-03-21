using System.Collections.Generic;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public abstract class DecimalValueObject : ValueObject
    {
        protected DecimalValueObject(decimal value)
        {
            Value = value;
        }

        public decimal Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
