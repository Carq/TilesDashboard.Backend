using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NCrontab;
using TilesDashboard.Handy.Tools;
using TilesDashboard.PluginBase.Data;
using TilesDashboard.PluginSystem.Entities;
using TilesDashboard.PluginSystem.Repositories;
using TilesDashboard.PluginSystem.Services;
using TilesDashboard.WebApi.PluginSystem.Extensions;
using TilesDashboard.WebApi.PluginSystem.Loaders;

namespace TilesDashboard.WebApi.BackgroundWorkers
{
    public class PluginBackgroundWorker : BackgroundService
    {
        private readonly ILogger<PluginBackgroundWorker> _logger;

        private readonly IPluginConfigRepository _pluginConfigRepository;

        private readonly IDataPluginLoader _pluginLoader;

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly IDataPluginService _dataPluginService;

        public PluginBackgroundWorker(
            IDataPluginLoader pluginLoader,
            ILogger<PluginBackgroundWorker> logger,
            IDateTimeProvider dateTimeProvider,
            IPluginConfigRepository pluginConfigRepository,
            IDataPluginService dataPluginService)
        {
            _pluginLoader = pluginLoader ?? throw new ArgumentNullException(nameof(pluginLoader));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _dateTimeProvider = dateTimeProvider ?? throw new ArgumentNullException(nameof(dateTimeProvider));
            _pluginConfigRepository = pluginConfigRepository ?? throw new ArgumentNullException(nameof(pluginConfigRepository));
            _dataPluginService = dataPluginService;
        }

        [SuppressMessage("Microsoft.Naming", "CA1725", Justification = "Allowed here")]
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Tiles background worker started.");
            var loadedPlugins = await _pluginLoader.LoadDataPluginsAsync(AppDomain.CurrentDomain.BaseDirectory);
            await InitializePluginsStorage(loadedPlugins, cancellationToken);
            var pluginsConfigurations = await _pluginConfigRepository.GetEnabledDataPluginsConfiguration(cancellationToken);

            foreach (var plugin in loadedPlugins)
            {
                var pluginConfig = pluginsConfigurations.SingleOrDefault(x => x.PluginName == plugin.UniquePluginName);
                if (pluginConfig != null)
                {
                    SchedulePlugin(plugin, pluginConfig, cancellationToken);
                }
            }

            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
            }
        }

        private async Task InitializePluginsStorage(IList<IDataPlugin> loadedPlugins, CancellationToken cancellationToken)
        {
            foreach (var plugin in loadedPlugins)
            {
                if (!await _pluginConfigRepository.IsAnyPluginConfigurationExist(plugin.UniquePluginName, cancellationToken))
                {
                    await _pluginConfigRepository.CreatePluginConfigurationWithTemplateEntry(plugin.UniquePluginName, plugin.TileType, plugin.PluginType, cancellationToken);
                }
            }
        }

        private void SchedulePlugin(IDataPlugin plugin, PluginConfiguration pluginConfiguration, CancellationToken cancellationToken)
        {
            foreach (var pluginConfigurationForTile in pluginConfiguration.PluginTileConfigs.Where(x => x.Disabled == false))
            {
                SchedulePluginForTile(plugin, pluginConfigurationForTile, cancellationToken);
            }
        }

        private void SchedulePluginForTile(IDataPlugin plugin, PluginTileConfig pluginConfigurationForTile, CancellationToken cancellationToken)
        {
            var schedule = CrontabSchedule.Parse(pluginConfigurationForTile.CronSchedule, new CrontabSchedule.ParseOptions { IncludingSeconds = true });
            DateTimeOffset nextOccurrence = schedule.GetNextOccurrence(_dateTimeProvider.Now.DateTime);
            _logger.LogInformation($"Plugin: \"{plugin.UniquePluginName}\" for Tile Id {pluginConfigurationForTile.TileStorageId} with Type {plugin.TileType} - Next schedule {nextOccurrence}");
            Observable.Timer(nextOccurrence)
                  .Select(x => HandlePlugin(plugin, pluginConfigurationForTile, cancellationToken))
                  .Switch()
                  .Subscribe(
                    plugin => SchedulePluginForTile(plugin, pluginConfigurationForTile, cancellationToken),
                    exception => _logger.LogError($"Error occurs during plugin processing. Plugin will be disabled. Error: {exception.Message}. Inner Exception: {exception.InnerException?.Message}", exception));
        }

        [SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "Allowed here")]
        private IObservable<IDataPlugin> HandlePlugin(IDataPlugin plugin, PluginTileConfig pluginConfigurationForTile, CancellationToken cancellationToken)
        {
            return Observable.Create<IDataPlugin>(
                async observer =>
                {
                    try
                    {
                        _logger.LogDebug($"Execute GetTileValueAsync method for plugin: \"{plugin.UniquePluginName}\" - \"{plugin.TileType}\"...");

                        PluginDataResult result = await _dataPluginService.HandlePlugin(plugin, pluginConfigurationForTile, cancellationToken);
                        if (result.Status.IsError())
                        {
                            observer.OnError(new InvalidOperationException($"Plugin {plugin.UniquePluginName} for tile with Id: \"{pluginConfigurationForTile.TileStorageId}\" return Status \"{result.Status}\" with message: \"{result.ErrorMessage}\""));
                            return;
                        }

                        if (result.Status == Status.NoUpdate)
                        {
                            _logger.LogDebug($"No data to update for plugin \"{plugin.UniquePluginName}\" and Tile with Id \"{pluginConfigurationForTile.TileStorageId}\".");
                        }

                        observer.OnNext(plugin);
                    }
                    catch (Exception exception)
                    {
                        observer.OnError(exception);
                    }
                });
        }
    }
}
