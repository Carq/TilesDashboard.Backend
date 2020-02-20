namespace MetricsDashboard.Contract
{
    public class SaveValueDto<TValue>
    {
        public string Name { get; set; }

        public TValue Value { get; set; }
    }
}
