using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Domain.Extensions;
using TilesDashboard.Core.Exceptions;
using TilesDashboard.Core.Storage;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Handy.Extensions;

namespace TilesDashboard.Core.Domain.Repositories
{
    public class TilesRepository : ITilesRepository
    {
        private readonly ITileContext _context;

        public TilesRepository(ITileContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IList<TileDbEntity>> GetAllTilesWithLimitedRecentData(int amountOfRecentData, CancellationToken cancellationToken)
        {
            var projection = Builders<TileDbEntity>.Projection.FetchRecentData(amountOfRecentData).FetchGroup().FetchConfiguration();

            return await _context.GetTiles().Find(_ => true)
                .Project<TileDbEntity>(projection)
                .ToListAsync(cancellationToken);
        }

        public async Task<TileDbEntity> GetTileWithLimitedRecentData(string tileName, TileType type, int amountOfRecentData, CancellationToken cancellationToken)
        {
            var filter = TileDbEntityExtensions.TileDbFilter(tileName, type);
            var projection = Builders<TileDbEntity>.Projection.FetchRecentData(amountOfRecentData).FetchGroup().FetchConfiguration();

            var tile = await _context.GetTiles()
                .Find(filter)
                .Project<TileDbEntity>(projection)
                .SingleOrDefaultAsync(cancellationToken);

            if (tile.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            return tile;
        }

        public async Task<TileDbEntity> GetTileDataForOneDay(string tileName, TileType type, DateTimeOffset nowDate, CancellationToken cancellationToken)
        {
            var filter = TileDbEntityExtensions.TileDbFilter(tileName, type);
            var onlyToday = Builders<BsonDocument>.Filter.And(
                Builders<BsonDocument>.Filter.Gte($"{nameof(TileDbEntity.Data)}.{nameof(TileData.AddedOn)}", nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)),
                Builders<BsonDocument>.Filter.Lt($"{nameof(TileDbEntity.Data)}.{nameof(TileData.AddedOn)}", nowDate.AddDays(1).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));

            var group = BsonSerializer.Deserialize<BsonDocument>($"{{ _id: \"$_id\", Data: {{ $push: \"$Data\" }} }}");

            return await _context.GetTiles()
                .Aggregate()
                .Match(filter)
                .Unwind(x => x.Data)
                .Match(onlyToday)
                .Group(group)
                .As<TileDbEntity>()
                .SingleOrDefaultAsync(cancellationToken);
        }

        public async Task<TileDbEntity> GetTileWithoutData(string tileName, TileType tileType, CancellationToken cancellationToken)
        {
            var projection = Builders<TileDbEntity>.Projection.FetchRecentData(0).FetchGroup().FetchConfiguration();
            var tile = await _context.GetTiles().Find(TileDbEntityExtensions.TileDbFilter(tileName, tileType)).Project<TileDbEntity>(projection).SingleOrDefaultAsync(cancellationToken);
            if (tile.NotExists())
            {
                throw new NotFoundException($"Tile {tileName} does not exist.");
            }

            return tile;
        }

        public async Task InsertData(string tileName, TileType tileType, BsonDocument newData, CancellationToken cancellationToken)
        {
            await _context.GetTiles().UpdateOneAsync(
                TileDbEntityExtensions.TileDbFilter(tileName, tileType),
                Builders<TileDbEntity>.Update.Push(x => x.Data, newData.ToBsonDocument()),
                null,
                cancellationToken);
        }
    }
}
