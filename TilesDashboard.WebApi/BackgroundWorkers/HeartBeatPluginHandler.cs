﻿using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.PluginBase;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginBase.Data.HeartBeatPlugin;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class HeartBeatPluginHandler
    {
        private readonly ILogger<HeartBeatPluginHandler> _logger;

        private readonly IHeartBeatService _heartBeatService;

        public HeartBeatPluginHandler(ILogger<HeartBeatPluginHandler> logger, IHeartBeatService heartBeatService)
        {
            _logger = logger;
            _heartBeatService = heartBeatService;
        }

        public async Task<PluginDataResult> HandlePlugin(HeartBeatDataPlugin plugin, PluginTileConfig pluginConfigForTile, CancellationToken cancellationToken)
        {
            var data = await plugin.GetTileValueAsync(pluginConfigForTile.Configuration, cancellationToken);
            _logger.LogDebug($"Heartbeat plugin: \"{plugin.UniquePluginName}\", Value: {data.Value}");
            if (data.Status.Is(Status.OK))
            {
                await _heartBeatService.RecordValue(new StorageId(pluginConfigForTile.TileStorageId), data.Value, data.AppVersion, data.AdditionalInfo);
            }

            return data;
        }
    }
}
