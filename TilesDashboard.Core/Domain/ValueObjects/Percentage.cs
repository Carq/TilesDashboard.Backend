using System;
using Dawn;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public class Percentage : DecimalValueObject
    {
        public const decimal Min = 0;

        public const decimal Max = 100;

        public Percentage(decimal value)
            : base(value)
        {
            Validate(value);
        }

        public static Percentage Zero => new Percentage(0);

        public static void Validate(decimal value)
        {
            Guard.Argument(value, nameof(Percentage)).InRange(Min, Max);
        }

        public decimal GetRoundedValue()
        {
            return Math.Round(Value, 0);
        }
    }
}
