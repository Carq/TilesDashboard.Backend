using System;

namespace MetricsDashboard.WebApi.Entities
{
    public class MetricHistory
    {
        public MetricHistory(int value, DateTimeOffset addedOn)
        {
            Value = value;
            AddedOn = addedOn;
        }

        public int Id { get; private set; }

        public int Value { get; private set; }

        public Metric Metric { get; private set; }

        public DateTimeOffset AddedOn { get; private set; }
    }
}
