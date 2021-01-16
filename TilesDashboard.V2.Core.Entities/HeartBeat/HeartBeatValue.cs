using System;

namespace TilesDashboard.V2.Core.Entities.HeartBeat
{
    public class HeartBeatValue : TileValue
    {
        public const int Unavailable = -1;

        public HeartBeatValue(int responseTimeInMs, string appVersion, string additionalInfo, DateTimeOffset addedOn)
            : base(addedOn)
        {
            ResponseTimeInMs = ValidateResponseTime(responseTimeInMs);
            AppVersion = ValidateAppVersion(appVersion);
            AdditionalInfo = additionalInfo;
        }

        /// <summary>
        /// Application response time in ms. -1 means that application is unavailable.
        /// </summary>
        public int ResponseTimeInMs { get; private set; }

        public string AppVersion { get; private set; }

        public string AdditionalInfo { get; private set; }

        private static int ValidateResponseTime(int responseTimeInMs)
        {
            if (responseTimeInMs == Unavailable || responseTimeInMs > 0)
            {
                return responseTimeInMs;
            }

            throw new ArgumentOutOfRangeException($"Response time has to be greater than 0 or -1 (it means that service is unavailable). Given value is {responseTimeInMs}.");
        }

        private static string ValidateAppVersion(string appVersion)
        {
            if (string.IsNullOrWhiteSpace(appVersion))
            {
                throw new ArgumentOutOfRangeException($"App version has to be provided. Given value is {appVersion}.");
            }

            return appVersion;
        }
    }
}
