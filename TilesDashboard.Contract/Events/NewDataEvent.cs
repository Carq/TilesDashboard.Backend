using TilesDashboard.Handy.Events;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(TileId tileId, TileValue tileValue)
        {
            TileId = tileId;
            TileValue = tileValue;
        }

        public TileId TileId { get; }

        public TileValue TileValue { get; }
    }
}
