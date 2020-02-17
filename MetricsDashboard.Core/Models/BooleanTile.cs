using System.Collections.Generic;
using System.Linq;
using MetricsDashboard.Contract;
using MetricsDashboard.Contract.Enums;
using MetricsDashboard.Core.Entities;

namespace MetricsDashboard.Core.Models
{
    public class BooleanTile : ITile
    {
        private readonly string _name;

        private readonly MetricEntity<bool> _latest;

        public BooleanTile(string name, IEnumerable<MetricEntity<bool>> metrics)
        {
            _name = name;
            _latest = metrics.OrderByDescending(m => m.AddedOn).First();
        }

        public TileDto ToDto()
        {
            return new TileDto
            {
                Name = _name,
                TileType = TileType.Boolean,
                TileData = new BooleanResultDto
                {
                    IsSuccess = _latest.Value,
                    LastUpdated = _latest.AddedOn,
                },
            };
        }
    }
}