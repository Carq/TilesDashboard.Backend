using System;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public class Temperature : DecimalValueObject
    {
        public Temperature(decimal value)
            : base(value)
        {
        }

        public decimal GetRoundedValue()
        {
            return Math.Round(Value, 1);
        }
    }
}
