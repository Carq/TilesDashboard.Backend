using Dawn;

namespace TilesDashboard.Core.Domain.ValueObjects
{
    public class Humidity : DecimalValueObject
    {
        public const decimal Min = 0;

        public const decimal Max = 1;

        public Humidity(decimal value)
            : base(value)
        {
            Guard.Argument(value, nameof(Humidity)).InRange(Min, Max);
        }
    }
}
