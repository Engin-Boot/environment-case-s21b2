
using Xunit;

namespace AlertSystem.Test
{
    public class MapGeneratorUnitTest
    {
        [Fact]
        public void CheckParameterMapsAreNotNull()
        {
            Assert.NotNull(MapGenerator.TemperatureRangeMap);
            Assert.NotNull(MapGenerator.HumidityRangeMap);
        }
    }
}
