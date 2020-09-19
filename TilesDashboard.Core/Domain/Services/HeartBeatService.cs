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
    public class HeartBeatService : TileService, IHeartBeatService
    {
        private readonly IEventDispatcher _eventDispatcher;

        public HeartBeatService(ITileContext context, ITilesRepository tilesRepository, IDateTimeOffsetProvider dateTimeOffsetProvider, IEventDispatcher eventDispatcher)
            : base(context, tilesRepository, dateTimeOffsetProvider)
        {
            _eventDispatcher = eventDispatcher ?? throw new System.ArgumentNullException(nameof(eventDispatcher));
        }

        public async Task RecordDataAsync(string tileName, int responseInMs, string appVersion, CancellationToken cancellationToken)
        {
            var heartBeatData = new HeartBeatData(responseInMs, appVersion, DateTimeOffsetProvider.Now);

            await TilesRepository.InsertData(tileName, TileType.HeartBeat, heartBeatData.ToBsonDocument(), cancellationToken);
            await _eventDispatcher.PublishAsync(new NewDataEvent(new TileId(tileName, TileType.HeartBeat), new { heartBeatData.ResponseTimeInMs, heartBeatData.AppVersion, heartBeatData.AddedOn }), cancellationToken);
        }
    }
}
