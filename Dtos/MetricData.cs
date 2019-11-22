using MetricsDashboard.WebApi.Entities.Enums;

namespace MetricsDashboard.WebApi.Dtos
{
    public class MetricData
    {
        public MetricData(int id, string name, int limit, int? wish, int? goal, MetricType type)
        {
            Id = id;
            Name = name;
            Limit = limit;
            Wish = wish;
            Goal = goal;
            Type = type;
        }

        public int Id { get;  }

        public string Name { get; }

        public int Limit { get; }

        public int? Wish { get; }

        public int? Goal { get; }

        public MetricType Type { get; }
    }
}
