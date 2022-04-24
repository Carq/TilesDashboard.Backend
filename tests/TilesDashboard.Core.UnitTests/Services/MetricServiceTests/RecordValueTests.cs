using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using TilesDashboard.Core.UnitTests.TestClassBuilders;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.Handy.Tools;
using TilesDashboard.TestUtils;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Metric;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.Core.UnitTests.Services.MetricServiceTests
{
    internal class RecordValueTests : TestBase<MetricService>
    {
        [Test]
        public async Task ShouldSaveMatricPercentageValueWithValidAddedOnDate()
        {
            // given
            var percentageValue = 25.1m;
            var metricTileId = new TileId("API", TileType.Metric);
            var currentTime = DateTimeOffsetTestData.June28Year2020At0639;

            A.CallTo(() => M<IDateTimeProvider>().Now).Returns(currentTime);
            A.CallTo(() => Resolve<ITileRepository>().GetTile<MetricTile>(metricTileId))
                .Returns(Task.FromResult(MetricTileTestBuilder.NewPercentageMetric(metricTileId.Name).Build()));

            // when
            await TestCandidate.RecordValue(metricTileId, MetricType.Percentage, percentageValue);

            // then
            A.CallTo(() => Resolve<ITileRepository>()
                                .RecordValue(
                                    metricTileId,
                                    A<PercentageMetricValue>.That.Matches(x =>
                                        x.Value == percentageValue &&
                                        x.AddedOn == currentTime)))
                                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task ShouldSaveMetricMoneyValueWithValidAddedOnDate()
        {
            // given
            var moneyValue = 21m;
            var metricTileId = new TileId("API", TileType.Metric);
            var currentTime = DateTimeOffsetTestData.June28Year2020At0639;

            A.CallTo(() => M<IDateTimeProvider>().Now).Returns(currentTime);
            A.CallTo(() => Resolve<ITileRepository>().GetTile<MetricTile>(metricTileId))
                .Returns(Task.FromResult(MetricTileTestBuilder.NewMoneyMetric(metricTileId.Name).Build()));

            // when
            await TestCandidate.RecordValue(metricTileId, MetricType.Money, moneyValue);

            // then
            A.CallTo(() => Resolve<ITileRepository>()
                                .RecordValue(
                                    metricTileId,
                                    A<MoneyMetricValue>.That.Matches(x =>
                                        x.Value == moneyValue &&
                                        x.AddedOn == currentTime)))
                                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public async Task ShouldSaveMetrictTimeValueWithValidAddedOnDate()
        {
            // given
            var timeValue = 15.21m;
            var metricTileId = new TileId("API", TileType.Metric);
            var currentTime = DateTimeOffsetTestData.June28Year2020At0639;

            A.CallTo(() => M<IDateTimeProvider>().Now).Returns(currentTime);
            A.CallTo(() => Resolve<ITileRepository>().GetTile<MetricTile>(metricTileId))
                .Returns(Task.FromResult(MetricTileTestBuilder.NewTimeMetric(metricTileId.Name).Build()));

            // when
            await TestCandidate.RecordValue(metricTileId, MetricType.Time, timeValue);

            // then
            A.CallTo(() => Resolve<ITileRepository>()
                                .RecordValue(
                                    metricTileId,
                                    A<TimeMetricValue>.That.Matches(x =>
                                        x.Value == timeValue &&
                                        x.AddedOn == currentTime)))
                                .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void ShouldSThrowNotSupportedException_WhenMetricTypeIsUnspecified()
        {
            // given
            var metricTileId = new TileId("API", TileType.Metric);

            A.CallTo(() => Resolve<ITileRepository>().GetTile<MetricTile>(metricTileId))
                .Returns(Task.FromResult(MetricTileTestBuilder
                    .New()
                    .WithConfigEntry(nameof(MetricConfiguration.MetricType), MetricType.Unspecified)
                    .Build()));

            // when
            Func<Task> action = async () => await TestCandidate.RecordValue(metricTileId, MetricType.Time, 66m);

            // then
            action.Should().ThrowExactlyAsync<NotSupportedException>();
        }
    }
}
