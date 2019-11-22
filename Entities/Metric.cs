using System;
using System.Collections.Generic;
using MetricsDashboard.WebApi.Entities.Enums;

namespace MetricsDashboard.WebApi.Entities
{
    public class Metric
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public MetricType Type { get; private set; }

        public int Limit { get; private set; }

        public int? Wish { get; private set; }

        public int? Goal { get; private set; }

        public IList<MetricHistory> History { get; private set; }

        public void SaveValue(in int value, DateTimeOffset date)
        {
            History.Add(new MetricHistory(value, date));
        }
    }
}
