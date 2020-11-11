using System;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public class PercentageMetricValue : MetricValue
    {
        public const decimal Min = 0;

        public const decimal Max = 100;

        public PercentageMetricValue(decimal value, DateTimeOffset addedOn) : base(addedOn)
        {
            Value = ParseAndValidate(value);
        }

        protected static decimal ParseAndValidate(decimal value)
        {
            value = Math.Round(value, 1);
            if (value < Min || value > Max)
            {
                throw new ArgumentOutOfRangeException($"Percentage value of metric must be between 0% and 100%. Given value is {value}.");
            }

            return value;
        }
    }
}
