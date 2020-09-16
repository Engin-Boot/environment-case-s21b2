using System;
using Xunit;

namespace AlertSystem.Test
{
    public class RangeCheckerUnitTest
    {
        private readonly MapRangeToParameterStatus[] temperatureRangeMap;
        public RangeCheckerUnitTest()
        {
            temperatureRangeMap = new MapRangeToParameterStatus[4];

            temperatureRangeMap[0] =
                new MapRangeToParameterStatus(int.MinValue, -1, ParameterStatus.VeryLow, "is very low!");

            temperatureRangeMap[1] =
                new MapRangeToParameterStatus(0, 3, ParameterStatus.Low, "is low!");

            temperatureRangeMap[2] =
                new MapRangeToParameterStatus(38, 40, ParameterStatus.High, "is high!");

            temperatureRangeMap[3] =
                new MapRangeToParameterStatus(41, int.MaxValue, ParameterStatus.VeryHigh, "is very high!");

        }

        [Fact]
        public void WhenTemperatureIsNormalThenCalculateRangeResult()
        {
            RangeChecker checker = new RangeChecker("Temperature", temperatureRangeMap);

            RangeResult result = checker.CalculateParameterRangeResult(25);

            Assert.Equal(ParameterStatus.Normal, result.Status);
            Assert.Equal("", result.Message);
        }
        
        [Fact]
        public void WhenTemperatureIsLowThenCalculateRangeResult()
        {
            RangeChecker checker = new RangeChecker("Temperature", temperatureRangeMap);

            RangeResult result = checker.CalculateParameterRangeResult(2);

            Assert.Equal(ParameterStatus.Low, result.Status);
            Assert.Equal("Temperature is low!", result.Message);
        }

    }
}
