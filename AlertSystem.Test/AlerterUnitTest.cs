using Xunit;

namespace AlertSystem.Test
{
    public class AlerterUnitTest
    {
        private readonly IAlerter _csvAlerter;

        public AlerterUnitTest()
        {
            _csvAlerter = new AlertByReportToCsvFile("EnvironmentIssuesReport.csv");
        }

        [Fact]
        public void TestSendAlert()
        {
            _csvAlerter.SendAlert("Humidity", ParameterStatus.VeryHigh, BreachLevel.Error);
            _csvAlerter.SendAlert("Temperature", ParameterStatus.Low, BreachLevel.Warning);
            _csvAlerter.Dispose();
        }
        
    }
}
