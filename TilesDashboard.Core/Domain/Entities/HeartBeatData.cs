using System;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Entities
{
    public class HeartBeatData : TileData
    {
        public HeartBeatData(int responseTime, string appVersion, string additionalInfo, DateTimeOffset addedOn)
           : this(responseTime, appVersion, addedOn)
        {
            AdditionalInfo = additionalInfo;
        }

        public HeartBeatData(int responseTime, string appVersion, DateTimeOffset addedOn)
            : this(responseTime, addedOn)
        {
            AppVersion = appVersion;
        }

        public HeartBeatData(int responseTime, DateTimeOffset addedOn)
            : base(addedOn)
        {
            ResponseTimeInMs = responseTime;
        }

        /// <summary>
        /// Application response time in ms. -1 means that application is unavailable.
        /// </summary>
        public int ResponseTimeInMs { get; private set; }

        public string AppVersion { get; private set; }

        public string AdditionalInfo { get; private set; }

        public static HeartBeatData Unavailable(DateTimeOffset addedOn) => new HeartBeatData(-1, addedOn);
    }
}
