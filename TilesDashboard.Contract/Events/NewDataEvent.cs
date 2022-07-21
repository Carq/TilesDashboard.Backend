using TilesDashboard.Handy.Events;
using TilesDashboard.V2.Core.Entities;

namespace TilesDashboard.Contract.Events
{
    public class NewDataEvent : IEvent
    {
        public NewDataEvent(TileId tileId, StorageId tileStorageId, TileValue tileValue)
        {
            TileId = tileId;
            TileStorageId = tileStorageId;
            TileValue = tileValue;
        }

        public TileId TileId { get; }

        public StorageId TileStorageId { get; }

        public TileValue TileValue { get; }
    }
}
