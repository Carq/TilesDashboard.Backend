using System;

namespace TilesDashboard.PluginBase
{
    /// <summary>
    /// Base class which represents output of each plugin.
    /// </summary>
    public abstract class Result
    {
        protected Result(Status status)
        {
            Status = status;
        }

        protected Result(Status status, DateTimeOffset dateOfChange)
        {
            Status = status;
            DateOfChange = dateOfChange;
        }

        /// <summary>
        /// Status of getting data.
        /// OK - data should be saved.
        /// NoUpdate - no new data, saving operation will be to performed.
        /// </summary>
        public Status Status { get; }

        public DateTimeOffset? DateOfChange { get; }
    }
}
