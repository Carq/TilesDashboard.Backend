namespace MetricsDashboard.Core.Configuration
{
    public interface IDatabaseSettings
    {
        string MongoConnectionString { get; }

        string DatabaseName { get; }
    }
}
