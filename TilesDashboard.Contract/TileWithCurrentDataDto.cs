using System.Collections.Generic;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract
{
    public class TileWithCurrentDataDto
    {
        public string Name { get; set; }

        public TileTypeDto Type { get; set; }

        public object CurrentData { get; set; }

        public IList<object> RecentData { get; } = new List<object>();

        public object Configuration { get; set; }
    }
}