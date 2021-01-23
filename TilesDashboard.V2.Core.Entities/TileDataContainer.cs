using System;
using System.Collections.Generic;
using System.Globalization;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.V2.Core.Entities
{
    public class TileDataContainer
    {
        public string TileStorageId { get; private set; }

        public TileType Type { get; private set; }

        public string GroupDate { get; private set; }

        public IList<TileValue> Data { get; private set; }

        public static string GenerateGroupDate(DateTimeOffset date)
        {
            return date.ToString("yyyy-MM", CultureInfo.InvariantCulture);
        }
    }
}
