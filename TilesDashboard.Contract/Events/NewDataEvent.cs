using TilesDashboard.Contract.Enums;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(string tileName, TileTypeDto tileType, object newValue)
        {
            TileName = tileName;
            TileType = tileType;
            NewValue = newValue;
        }

        public string TileName { get; }

        public TileTypeDto TileType { get; }

        public object NewValue { get; }
    }
}
