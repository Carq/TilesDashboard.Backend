using System;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public class Temperature : DecimalValueObject
    {
        public Temperature(decimal value)
            : base(value)
        {
        }

        public static Temperature Zero => new Temperature(0);

        public decimal GetRoundedValue()
        {
            return Math.Round(Value, 1);
        }
    }
}
