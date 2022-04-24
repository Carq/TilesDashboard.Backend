using NUnit.Framework;
using TilesDashboard.Handy.Tools;
using FluentAssertions;
using System;

namespace TilesDashboard.Handy.UnitTests.Tools
{
    internal class DateTimeOffsetProviderTests
    {
        [Test]
        public void ShouldThrowException_WhenNullListIsNull()
        {
            // given && when && then
            new DateTimeOffsetProvider().Now.Should().BeCloseTo(DateTimeOffset.Now, TimeSpan.FromSeconds(1));
        }
    }
}
