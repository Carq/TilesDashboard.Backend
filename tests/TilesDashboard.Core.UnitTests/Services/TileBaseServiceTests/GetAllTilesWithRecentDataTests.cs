using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TilesDashboard.Core.UnitTests.TestClassBuilders;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.Handy.Extensions;
using TilesDashboard.TestUtils;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.Core.UnitTests.Services.TileBaseServiceTests
{
    public class GetAllTilesWithRecentDataTests : TestBase<TileBaseService>
    {
        [Test]
        public async Task ShouldReturnMetricTile_WhenThereIsNoTileValues()
        {
            // given
            var tileName = "NiceTile";
            var metricTile = MetricTileTestBuilder.New(tileName).Build();

            A.CallTo(() => M<ITileRepository>().GetAll()).Returns(Task.FromResult((metricTile as TileEntity).ToOneElementList()));
            A.CallTo(() => M<ITileRepository>().GetRecentTileValues(metricTile.TileId, A<int>._)).Returns(Task.FromResult<IList<TileValue>>(null));

            // when
            var result = await TestCandidate.GetAllTilesWithRecentData();

            // then
            result.Should().HaveCount(1);
            result[0].TileEntity.TileId.Name.Should().Be(tileName);
            result[0].TileEntity.TileId.Type.Should().Be(TileType.Metric);
            result[0].Data.Should().BeEmpty();
        }

        [Test]
        public async Task ShouldReturn2TilesWithTileValues()
        {
            // given
            var metricName = "metricName";
            var weatherName = "weatherName";
            var metricTile = MetricTileTestBuilder.NewPercentageMetric(metricName).Build();
            var metricValue1 = 80m;
            var metrciValue2 = 50m;

            var weatherTile = WeatherTileTestBuilder.New(weatherName).Build();
            var temperature = 22.2m;
            var humidity = 43m;

            A.CallTo(() => M<ITileRepository>().GetAll()).Returns(
                Task.FromResult<IList<TileEntity>>(
                    new List<TileEntity> {
                        metricTile,
                        weatherTile}));

            A.CallTo(() => M<ITileRepository>().GetRecentTileValues(metricTile.TileId, A<int>._)).Returns(
                Task.FromResult<IList<TileValue>>
                (
                    new TileValue[]
                    {
                        new PercentageMetricValue(metricValue1, DateTimeOffsetTestData.June28Year2020At0639),
                        new PercentageMetricValue(metrciValue2, DateTimeOffsetTestData.June28Year2020At0639.AddHours(1))
                    }
                ));

            A.CallTo(() => M<ITileRepository>().GetRecentTileValues(weatherTile.TileId, A<int>._)).Returns(
              Task.FromResult<IList<TileValue>>
              (
                  new TileValue[]
                  {
                    new WeatherValue(temperature, humidity, DateTimeOffsetTestData.April02Year2020),
                  }
              ));

            // when
            var result = (await TestCandidate.GetAllTilesWithRecentData()).OrderBy(x => x.TileEntity.TileId.Name).ToList();

            // then
            result.Should().HaveCount(2);

            var metricTileResult = result[0];
            metricTileResult.TileEntity.TileId.Name.Should().Be(metricName);
            metricTileResult.TileEntity.TileId.Type.Should().Be(TileType.Metric);
            metricTileResult.Data.Should().HaveCount(2);
            metricTileResult.Data[0].AddedOn.Should().Be(DateTimeOffsetTestData.June28Year2020At0639);
            (metricTileResult.Data[0] as PercentageMetricValue)?.Value.Should().Be(metricValue1);
            metricTileResult.Data[1].AddedOn.Should().Be(DateTimeOffsetTestData.June28Year2020At0639.AddHours(1));
            (metricTileResult.Data[1] as PercentageMetricValue)?.Value.Should().Be(metrciValue2);

            var weatherTileResult = result[1];
            weatherTileResult.TileEntity.TileId.Name.Should().Be(weatherName);
            weatherTileResult.TileEntity.TileId.Type.Should().Be(TileType.Weather);
            weatherTileResult.Data.Should().HaveCount(1);
            weatherTileResult.Data[0].AddedOn.Should().Be(DateTimeOffsetTestData.April02Year2020);
            (weatherTileResult.Data[0] as WeatherValue)?.Temperature.Should().Be(temperature);
            (weatherTileResult.Data[0] as WeatherValue)?.Humidity.Should().Be(humidity);
        }
    }
}
