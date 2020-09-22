namespace TilesDashboard.PluginBase.Data.HeartBeatPlugin
{
    public class HeartBeatData : Result
    {
        public HeartBeatData(Status status) 
            : base(status)
        {
        }

        public HeartBeatData(int value, string appVersion, string additionalInfo, Status status)
            : base(status)
        {
            Value = value;
            AppVersion = appVersion;
            AdditionalInfo = additionalInfo;
        }

        public static HeartBeatData NoResponse() => new HeartBeatData(-1, null, null, Status.OK);

        public static HeartBeatData Error(string errorMessage) => new HeartBeatData(Status.Error).WithErrorMessage(errorMessage) as HeartBeatData;

        public static HeartBeatData NoUpdate() => new HeartBeatData(Status.NoUpdate);

        public int Value { get; }

        public string AppVersion { get; }

        public string AdditionalInfo { get; }

        public override string ToString()
        {
            return $"ResponseInMs: {Value} - Status: {Status}, AppVersion: {AppVersion}, AdditionalInfo: {AdditionalInfo}";
        }
    }
}
