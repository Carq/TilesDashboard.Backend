using System;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public abstract class MetricValue : ITileValue
    {
        protected MetricValue(DateTimeOffset addedOn)
        {
            AddedOn = addedOn;
        }

        public decimal Value { get; protected set; }

        public DateTimeOffset AddedOn { get; }
    }
}
