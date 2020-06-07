using System.Collections.Generic;
using TilesDashboard.Contract.Enums;

namespace TilesDashboard.Contract
{
    public class TileWithCurrentDataDto
    {
        public string Name { get; set; }

        public TileTypeDto Type { get; set; }

        public IList<object> Data { get; } = new List<object>();

        public GroupDto Group { get; set; }

        public object Configuration { get; set; }
    }
}