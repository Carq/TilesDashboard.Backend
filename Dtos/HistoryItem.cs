namespace MetricsDashboard.WebApi.Dtos
{
    public class HistoryItem
    {
        public HistoryItem(int value, string addedOn)
        {
            Value = value;
            AddedOn = addedOn;
        }

        public int Value { get; private set; }

        public string AddedOn { get; private set; }
    }
}
