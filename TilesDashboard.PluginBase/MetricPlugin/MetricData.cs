using System;

namespace TilesDashboard.PluginBase.MetricPlugin
{
    public class MetricData : Result
    {
        public MetricData(Status status) : base(status)
        {
        }

        public MetricData(Status status, DateTimeOffset dateOfChange) : base(status, dateOfChange)
        {
        }
    }
}
