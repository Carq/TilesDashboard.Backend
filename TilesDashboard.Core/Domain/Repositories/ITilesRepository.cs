using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using TilesDashboard.Core.Domain.Enums;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Repositories
{
    public interface ITilesRepository
    {
        Task<IList<TileDbEntity>> GetAllTilesWithLimitedRecentData(int amountOfRecentData, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileWithLimitedRecentData(string tileName, TileType type, int amountOfRecentData, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileDataForOneDay(string tileName, TileType type, DateTimeOffset nowDate, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileWithoutData(string tileName, TileType tileType, CancellationToken cancellationToken);

        Task InsertData(string tileName, TileType tileType, BsonDocument newData, CancellationToken cancellationToken);
    }
}