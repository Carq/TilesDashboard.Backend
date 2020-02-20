﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MetricsDashboard.Contract;

namespace MetricsDashboard.Core
{
    public interface ITileService
    {
        Task<IList<TileDto>> GetAllTilesAsync(CancellationToken cancellationToken);
    }
}