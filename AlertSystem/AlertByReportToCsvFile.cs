using System.Collections.Generic;
using System.Globalization;
using Microsoft.VisualBasic;

namespace AlertSystem
{
    public class AlertByReportToCsvFile : IAlerter
    {
        private readonly CsvFileWriter _writer;

        public AlertByReportToCsvFile(string fileName)
        {
            _writer = new CsvFileWriter(fileName);
            var columns = new List<string> { "Date and Time", "Parameter", "Parameter Status", "Breach Level" };
            _writer.WriteRow(columns);

        }

        public void Dispose()
        {
            _writer.Dispose();
        }

        public void SendAlert(string parameter, ParameterStatus status, BreachLevel level)
        {

            var columns = new List<string>
            {
                DateAndTime.Now.ToString(CultureInfo.InvariantCulture),
                parameter,
                status.ToString(),
                level.ToString()
            };
            _writer.WriteRow(columns);
        }
    }
}
