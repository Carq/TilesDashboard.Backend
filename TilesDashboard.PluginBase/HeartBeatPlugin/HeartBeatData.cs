namespace TilesDashboard.PluginBase.IntegerPlugin
{
    public class HeartBeatData : Result
    {
        public HeartBeatData(Status status) : base(status)
        {
        }

        public HeartBeatData(int value, Status status) : base(status)
        {
            Value = value;
        }

        public static HeartBeatData Error(string errorMessage) => new HeartBeatData(Status.Error).WithErrorMessage(errorMessage) as HeartBeatData;

        public static HeartBeatData NoUpdate() => new HeartBeatData(Status.NoUpdate);

        public int Value { get; }

        public override string ToString()
        {
            return $"ResponseInMs: {Value} - Status: {Status}";
        }
    }
}
