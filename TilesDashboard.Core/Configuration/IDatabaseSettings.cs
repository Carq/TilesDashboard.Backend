namespace TilesDashboard.Core.Configuration
{
    public interface IDatabaseSettings
    {
        string MongoConnectionString { get; }

        string DatabaseName { get; }
    }
}
