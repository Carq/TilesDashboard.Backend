using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Threading.Tasks;
using TilesDashboard.Core.UnitTests.TestClassBuilders;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.Core.UnitTests.Services.TileBaseServiceTests
{
    public class GetAllTilesWithRecentDataTests : TestBase<TileBaseService>
    {
        [Test]
        public async Task ShouldReturnMetricTile()
        {
            // given
            var tileName = "NiceTile";
            var metricTile = MetricTileTestBuilder.New(tileName).Build();

            A.CallTo(() => M<ITileRepository>().GetAll()).Returns(Task.FromResult((metricTile as TileEntity).ToOneElementList()));

            // when
            var result = await TestCandidate.GetAllTilesWithRecentData();

            // then
            result.Should().HaveCount(1);
            result[0].TileEntity.TileId.Name.Should().Be(tileName);
            result[0].TileEntity.TileId.Type.Should().Be(TileType.Metric);
        }
    }
}
