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
                new MapRangeToParameterStatus(int.MinValue, -1, ParameterStatus.VeryLow, BreachLevel.Error);

            _temperatureRangeMap[1] =
                new MapRangeToParameterStatus(0, 3, ParameterStatus.Low, BreachLevel.Warning);

            _temperatureRangeMap[2] =
                new MapRangeToParameterStatus(38, 40, ParameterStatus.High, BreachLevel.Warning);

            _temperatureRangeMap[3] =
                new MapRangeToParameterStatus(41, int.MaxValue, ParameterStatus.VeryHigh, BreachLevel.Error);

        }

        [Fact]
        public void WhenTemperatureIsNormalThenCalculateRangeResult()
        {
            var checker = new RangeChecker("Temperature", _temperatureRangeMap, null);

            var result = checker.CalculateParameterRangeResult(25);

            Assert.Equal("Temperature", result.Parameter);
            Assert.Equal(BreachLevel.Safe, result.Level);
            Assert.Equal(ParameterStatus.Normal, result.Status);
        }
        
        [Fact]
        public void WhenTemperatureIsLowThenCalculateRangeResult()
        {
            var checker = new RangeChecker("Temperature", _temperatureRangeMap, null);

            var result = checker.CalculateParameterRangeResult(2);

            Assert.Equal("Temperature", result.Parameter);
            Assert.Equal(BreachLevel.Warning, result.Level);
            Assert.Equal(ParameterStatus.Low, result.Status);
        }

        [Fact]
        public void WhenRangeCheckerHasDelegateInstanceThenRunInvokeMethod()
        {
            
            var csvFileAlerter = new AlertByReportToCsvFile("RangeCheckerTestFile.csv");

            var rangeChecker = new RangeChecker("Temperature", _temperatureRangeMap, csvFileAlerter.SendAlert);

            rangeChecker.CalculateParameterRangeResult(-2);

            csvFileAlerter.Dispose();
        }

    }
}
