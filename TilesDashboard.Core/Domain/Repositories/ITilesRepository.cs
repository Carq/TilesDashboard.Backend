using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using TilesDashboard.Core.Storage.Entities;
using TilesDashboard.Core.Type.Enums;

namespace TilesDashboard.Core.Domain.Repositories
{
    public interface ITilesRepository
    {
        Task<IList<TileDbEntity>> GetAllTilesWithLimitedRecentData(int amountOfRecentData, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileWithLimitedRecentData(string tileName, TileType type, int amountOfRecentData, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileDataSince(string tileName, TileType type, DateTimeOffset sinceDate, CancellationToken cancellationToken);

        Task<TileDbEntity> GetTileWithoutData(string tileName, TileType tileType, CancellationToken cancellationToken);

        Task InsertData(string tileName, TileType tileType, BsonDocument newData, CancellationToken cancellationToken);
    }
}