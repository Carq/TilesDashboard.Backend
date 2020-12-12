using FakeItEasy;
using NUnit.Framework;
using System.Threading.Tasks;
using TilesDashboard.TestUtils;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.Core.UnitTests.Services.MetricServiceTests
{
    internal class GetMetricTileTests : TestBase<MetricService>
    {
        [Test]
        public async Task ShouldReturnTileDirectllyFromRepository()
        {
            // given
            var metricTileId = new TileId("API", TileType.Metric);

            // when
            await TestCandidate.GetMetricTile(metricTileId);

            // then
            A.CallTo(() => Resolve<ITileRepository>()
                                .GetTile<MetricTile>(metricTileId))
                                .MustHaveHappenedOnceExactly();
        }
    }
}
