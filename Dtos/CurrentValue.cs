using System;

namespace MetricsDashboard.WebApi.Dtos
{
    public class CurrentValue
    {
        public CurrentValue(int value, DateTimeOffset addedOn)
        {
            Value = value;
            AddedOn = addedOn;
        }

        public int Value { get; }

        public DateTimeOffset AddedOn { get; }
    }
}
