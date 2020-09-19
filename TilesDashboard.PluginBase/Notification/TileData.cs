using System;

namespace TilesDashboard.PluginBase.Notification
{
    public class TileData
    {
        protected TileData(DateTimeOffset addedOn)
        {
            AddedOn = addedOn;
        }

        public DateTimeOffset AddedOn { get; }
    }
}
