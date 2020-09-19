using TilesDashboard.Core.Type.Enums;
using TilesDashboard.Handy.Events;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(string tileName, TileType tileType, object newValue)
        {
            TileName = tileName;
            TileType = tileType;
            NewValue = newValue;
        }

        public string TileName { get; }

        public TileType TileType { get; }

        public object NewValue { get; }
    }
}
