namespace TilesDashboard.PluginBase.Data.DualPlugin
{
    public class DualData : PluginDataResult
    {
        public DualData(decimal primary, decimal secondary, Status status)
            : base(status)
        {
            Primary = primary;
            Secondary = secondary;
        }

        public DualData(decimal primary, Status status)
            : base(status)
        {
            Primary = primary;
        }

        public DualData(Status status)
          : base(status)
        {
        }

        public static DualData Error(string errorMessage) => new DualData(Status.Error).WithErrorMessage(errorMessage) as DualData;

        public static DualData NoUpdate => new DualData(Status.NoUpdate);

        public decimal Primary { get; private set; }

        public decimal Secondary { get; private set; }

        public override string ToString()
        {
            return $"Primary: {Primary} - Secondary - {Secondary} - Status: {Status}";
        }
    }
}
