using NUnit.Framework;
using FluentAssertions;
using System;
using TilesDashboard.Core.UnitTests.TestData;
using TilesDashboard.V2.Core.Entities.Weather;

namespace TilesDashboard.Core.UnitTests.Entities.Weather
{
    internal class WeatherValueTests
    {
        [Test]
        public void ShouldSetValue()
        {
            // given & when
            var weatherValue = new WeatherValue(5.123m, 95.123m, DateTimeOffsetTestData.April02Year2020);

            // then
            weatherValue.Temperature.Should().Be(5.1m);
            weatherValue.Humidity.Should().Be(95m);
            weatherValue.AddedOn.Should().Be(DateTimeOffsetTestData.April02Year2020);
        }

        [Test]
        public void ShouldThrowException_WhenHumidityIsBelowZero()
        {
            // given & when
            // ReSharper disable once ObjectCreationAsStatement - needed by test
            Action action = () => new WeatherValue(5.123m, -1m, DateTimeOffsetTestData.April02Year2020);

            // then
            action.Should().Throw<ArgumentOutOfRangeException>().And.Message.Contains("-1", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
