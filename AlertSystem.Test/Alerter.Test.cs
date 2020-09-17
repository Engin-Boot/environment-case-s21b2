using Xunit;

namespace AlertSystem.Test
{
    public class Alerter
    {
        private readonly IAlerter _csvAlerter;

        public Alerter()
        {
            _csvAlerter = new AlertByReportToCsvFile("EnvironmentIssuesReport.csv");
        }

        [Fact]
        public void TestSendAlert()
        {
            _csvAlerter.SendAlert("Humidity", ParameterStatus.VeryHigh, BreachLevel.Error);
            _csvAlerter.SendAlert("Temperature", ParameterStatus.Low, BreachLevel.Warning);
        }
        
    }
}
