using FluentAssertions;
using NUnit.Framework;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
namespace TilesDashboard.Core.UnitTests.Entities.Metric.TileIdTests
{
    internal class TileIdCompareTests
    {
        [Test]
        public void ShouldBeEqual_WhenTwoTileIdsHaveTheSameData()
        {
            // given
            var tileId1 = new TileId("Super tile", TileType.Metric);
            var tileId2 = new TileId("Super tile", TileType.Metric);

            // when & then
            (tileId1 == tileId2).Should().BeTrue();
            (tileId1 != tileId2).Should().BeFalse();
        }

        [Test]
        public void ShouldBeEqual_WhenTwoTileIdsHaveTheSameDataAndNameHasDifferentCase()
        {
            // given
            var tileId1 = new TileId("Super Tile", TileType.Metric);
            var tileId2 = new TileId("Super tile", TileType.Metric);

            // when & then
            (tileId1 == tileId2).Should().BeTrue();
            (tileId1 != tileId2).Should().BeFalse();
        }

        [Test]
        public void ShouldNotBeEqual_WhenTwoTileIdsHaveDifferentNames()
        {
            // given
            var tileId1 = new TileId("Cool tile", TileType.Metric);
            var tileId2 = new TileId("Super tile", TileType.Metric);

            // when & then
            (tileId1 == tileId2).Should().BeFalse();
            (tileId1 != tileId2).Should().BeTrue();
        }

        [Test]
        public void ShouldNotBeEqual_WhenTwoTileIdsHaveDifferentTileTypes()
        {
            // given
            var tileId1 = new TileId("Super tile", TileType.HeartBeat);
            var tileId2 = new TileId("Super tile", TileType.Metric);

            // when & then
            (tileId1 == tileId2).Should().BeFalse();
            (tileId1 != tileId2).Should().BeTrue();
        }

        [Test]
        public void ShouldNotBeEqual_WhenOneTileIsNull()
        {
            // given
            var tileId1 = new TileId("Super tile", TileType.HeartBeat);
            var tileId2 = (TileId)null;

            // when & then
            (tileId1 == tileId2).Should().BeFalse();
            (tileId1 != tileId2).Should().BeTrue();
            // ReSharper disable once ExpressionIsAlwaysNull
            (tileId1.Equals(tileId2)).Should().BeFalse();
        }

        [Test]
        public void ShouldBeEqual_WhenBothTilesAreNull()
        {
            // given
            var tileId1 = (TileId)null;
            var tileId2 = (TileId)null;

            // when & then
            (tileId1 == tileId2).Should().BeTrue();
            (tileId1 != tileId2).Should().BeFalse();
        }
    }
}
