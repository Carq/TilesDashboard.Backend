using TilesDashboard.Contract.Enums;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(string tileName, TileTypeDto tileType)
        {
            TileName = tileName;
            TileType = tileType;
        }

        public string TileName { get; }

        public TileTypeDto TileType { get; }
    }
}
