using System;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public class TimeMetricValue : MetricValue
    {
        public const decimal Min = 0;

        public TimeMetricValue(decimal value, DateTimeOffset addedOn) : base(addedOn)
        {
            Value = ParseAndValidate(value);
        }

        protected static decimal ParseAndValidate(decimal value)
        {
            value = Math.Round(value, 2);
            if (value < Min)
            {
                throw new ArgumentOutOfRangeException($"Money value of metric cannot be less than 0. Given value is {value}.");
            }

            return value;
        }
    }
}
