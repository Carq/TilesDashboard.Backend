namespace TilesDashboard.PluginBase.Data.IntegerPlugin
{
    public class IntegerData : PluginDataResult
    {
        public IntegerData(Status status) : base(status)
        {
        }

        public IntegerData(int value, Status status) : base(status)
        {
            Value = value;
        }

        public static IntegerData Error(string errorMessage) => new IntegerData(Status.Error).WithErrorMessage(errorMessage) as IntegerData;

        public static IntegerData NoUpdate() => new IntegerData(Status.NoUpdate);

        public int Value { get; }

        public override string ToString()
        {
            return $"Value: {Value} - Status: {Status}";
        }
    }
}
