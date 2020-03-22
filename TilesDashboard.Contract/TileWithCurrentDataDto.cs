using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract
{
    public class TileWithCurrentDataDto
    {
        public string Name { get; set; }

        public TileTypeDto Type { get; set; }

        public object CurrentData { get; set; }

        public object Configuration { get; set; }
    }
}