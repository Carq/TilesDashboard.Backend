using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using TilesDashboard.Contract.Events;
using TilesDashboard.Core.Domain.Entities;
using TilesDashboard.Core.Domain.Repositories;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Type;
using TilesDashboard.Core.Type.Enums;
using TilesDashboard.Handy.Events;
using TilesDashboard.Handy.Tools;

namespace TilesDashboard.Core.Domain.Services
{
    public class IntegerTileService : TileService, IIntegerTileService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public IntegerTileService(ITileContext context, ITilesRepository tilesRepository, IDateTimeOffsetProvider dateTimeOffsetProvider, IEventDispatcher eventDispatcher)
            : base(context, tilesRepository, dateTimeOffsetProvider)
        {
            _eventDispatcher = eventDispatcher ?? throw new System.ArgumentNullException(nameof(eventDispatcher));
        }

        public async Task<IList<IntegerData>> GetIntegerRecentDataAsync(string tileName, int amountOfData, CancellationToken token)
        {
            return await GetRecentDataAsync<IntegerData>(tileName, TileType.Integer, amountOfData, token);
        }

        public async Task<IList<IntegerData>> GetIntegerDataSinceAsync(string tileName, int sinceDays, CancellationToken token)
        {
            return await GetDataSinceAsync<IntegerData>(tileName, TileType.Integer, DateTimeOffsetProvider.Now.AddDays(-sinceDays), token);
        }

        public async Task RecordIntegerDataAsync(string tileName, int value, CancellationToken cancellationToken)
        {
            var integerData = new IntegerData(value, DateTimeOffsetProvider.Now);

            await TilesRepository.InsertData(tileName, TileType.Integer, integerData.ToBsonDocument(), cancellationToken);
            await _eventDispatcher.PublishAsync(new NewDataEvent(new TileId(tileName, TileType.Integer), new { integerData.Value, integerData.AddedOn }), cancellationToken);
        }
    }
}
