using MongoDB.Driver;
using TilesDashboard.Core.Storage.Entities;

namespace TilesDashboard.Core.Domain.Extensions
{
    public static class TileDbEntityExtensions
    {
        public static IFindFluent<TileDbEntity, TileDbEntity> FetchRecentData(
            this IFindFluent<TileDbEntity, TileDbEntity> source, int amountOfData)
        {
            return source.Project<TileDbEntity>($"{{Data: {{ $slice: -{amountOfData} }} }}");
        }
    }
}