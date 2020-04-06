using System.Collections.Generic;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract
{
    public class TileDataDto
    {
        public string Name { get; set; }

        public TileTypeDto Type { get; set; }

        public IList<object> Data { get; private set; } = new List<object>();
    }
}
