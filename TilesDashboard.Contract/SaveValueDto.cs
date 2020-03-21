namespace TilesDashboard.Contract
{
    public class SaveValueDto<TValue>
    {
        public string TileName { get; set; }

        public TValue Value { get; set; }
    }
}
