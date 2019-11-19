namespace MetricsDashboard.WebApi.Entities
{
    public class MetricHistory
    {
        public int Id { get; set; }

        public int Value { get; set; }

        public Metric Metric { get; set; }
    }
}
