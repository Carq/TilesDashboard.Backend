using System;
using MetricsDashboard.WebApi.Entities.Enums;

namespace MetricsDashboard.WebApi.Dtos
{
    public class MetricData
    {
        public MetricData(int id, string name, int? current, int limit, int? goal, int? wish, DateTimeOffset? lastUppdated, MetricType type)
        {
            Id = id;
            Name = name;
            Limit = limit;
            Current = current;
            Wish = wish;
            Goal = goal;
            LastUpdated = lastUppdated;
            Type = type;
        }

        public int Id { get;  }

        public string Name { get; }

        public int? Current { get; }

        public int Limit { get; }

        public int? Wish { get; }

        public int? Goal { get; }

        public MetricType Type { get; }

        public DateTimeOffset? LastUpdated { get; }
    }
}
