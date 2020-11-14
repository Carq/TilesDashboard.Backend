using FluentAssertions;
using NUnit.Framework;
using System;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;

namespace TilesDashboard.Core.UnitTests.Entities.Metric.TileIdTests
{
    internal class TileIdCtorTests
    {
        [Test]
        public void ShouldCreateTileId()
        {
            // given
            var tileName = "Super tile";

            // when
            var tileId = new TileId(tileName, TileType.Metric);


            // then
            tileId.Name.Should().Be(tileName);
            tileId.Type.Should().Be(TileType.Metric);
            tileId.ToString().Should().Be($"{tileName} {TileType.Metric}");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void ShouldThrowException_WhenNameIsEmptyOrNull(string tileName)
        {
            // when
            Action action = () => new TileId(tileName, TileType.Metric);

            // then
            action.Should().ThrowExactly<ArgumentException>();
        }

        [Test]
        public void ShouldThrowException_WhenTileTypeIsUndefined()
        {
            // when
            Action action = () => new TileId("Just tile", TileType.Undefined);

            // then
            action.Should().ThrowExactly<ArgumentException>();
        }
    }
}
