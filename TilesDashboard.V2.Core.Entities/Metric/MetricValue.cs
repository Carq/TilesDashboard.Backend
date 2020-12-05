using System;

namespace TilesDashboard.V2.Core.Entities.Metric
{
    public abstract class MetricValue : TileValue
    {
        protected MetricValue(DateTimeOffset addedOn) : base(addedOn)
        {
        }

        public decimal Value { get; protected set; }
    }
}
