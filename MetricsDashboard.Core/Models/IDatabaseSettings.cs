namespace MetricsDashboard.Core.Models
{
    public interface IDatabaseSettings
    {
        string MongoConnectionString { get; }

        string DatabaseName { get; }
    }
}
