namespace TilesDashboard.V2.Core.Entities.Metric
{
    public interface IGenericTile
    {
        TileId TileId { get; }

        object TileConfiguration { get; }
    }
}