using System;

namespace TilesDashboard.Core.Type
{
    public class MetricData
    {
        public MetricData(decimal value, DateTimeOffset addedOn)
        {
            Value = Math.Round(value, 2);
            AddedOn = addedOn;
        }

        public decimal Value { get; private set; }

        public DateTimeOffset AddedOn { get; private set; }
    }
}
