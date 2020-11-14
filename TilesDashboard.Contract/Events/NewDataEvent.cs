using TilesDashboard.Handy.Events;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(TileId tileId, object newValue)
        {
            TileId = tileId;
            NewValue = newValue;
        }

        public TileId TileId { get; }

        public object NewValue { get; }
    }
}
