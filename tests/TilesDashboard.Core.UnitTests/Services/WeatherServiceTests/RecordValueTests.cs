using FakeItEasy;
using NUnit.Framework;
using System.Threading.Tasks;
using TilesDashboard.TestUtils;
using TilesDashboard.V2.Core.Entities;
using TilesDashboard.V2.Core.Entities.Enums;
using TilesDashboard.V2.Core.Entities.Weather;
using TilesDashboard.V2.Core.Repositories;
using TilesDashboard.V2.Core.Services;

namespace TilesDashboard.Core.UnitTests.Services.WeatherServiceTests
{
    internal class RecordValueTests : TestBase<WeatherService>
    {
        [Test]
        public async Task ShouldThrowArgumentNullException_WhenEventIsNull()
        {
            // given
            var temperature = 25.1m;
            var huminidity = 49m;
            var weatherTileId = new TileId("Gliwice", TileType.Weather);

            // when
            await TestCandidate.RecordValue(weatherTileId, temperature, huminidity);

            // Assert
            A.CallTo(() => Resolve<ITileRepository>()
                                .RecordValue(
                                    weatherTileId, 
                                    A<WeatherValue>.That.Matches(x => x.Temperature == temperature && x.Humidity == huminidity)))
                                .MustHaveHappenedOnceExactly();
        }
    }
}
