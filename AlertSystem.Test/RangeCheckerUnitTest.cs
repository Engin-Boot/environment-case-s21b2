using Xunit;

namespace AlertSystem.Test
{
    public class RangeCheckerUnitTest
    {
        private readonly MapRangeToParameterStatus[] _temperatureRangeMap;
        public RangeCheckerUnitTest()
        {
            _temperatureRangeMap = new MapRangeToParameterStatus[4];

            _temperatureRangeMap[0] =
                new MapRangeToParameterStatus(int.MinValue, -1, ParameterStatus.VeryLow, "is very low!");

            _temperatureRangeMap[1] =
                new MapRangeToParameterStatus(0, 3, ParameterStatus.Low, "is low!");

            _temperatureRangeMap[2] =
                new MapRangeToParameterStatus(38, 40, ParameterStatus.High, "is high!");

            _temperatureRangeMap[3] =
                new MapRangeToParameterStatus(41, int.MaxValue, ParameterStatus.VeryHigh, "is very high!");

        }

        [Fact]
        public void WhenTemperatureIsNormalThenCalculateRangeResult()
        {
            RangeChecker checker = new RangeChecker("Temperature", _temperatureRangeMap);

            RangeResult result = checker.CalculateParameterRangeResult(25);

            Assert.Equal(ParameterStatus.Normal, result.Status);
            Assert.Equal("", result.Message);
        }
        
        [Fact]
        public void WhenTemperatureIsLowThenCalculateRangeResult()
        {
            RangeChecker checker = new RangeChecker("Temperature", _temperatureRangeMap);

            RangeResult result = checker.CalculateParameterRangeResult(2);

            Assert.Equal(ParameterStatus.Low, result.Status);
            Assert.Equal("Temperature is low!", result.Message);
        }

    }
}
