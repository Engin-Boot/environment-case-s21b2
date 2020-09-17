using System.Collections.Generic;
using System.Globalization;
using CsvFileReaderAndWriter;
using Microsoft.VisualBasic;

namespace AlertSystem
{
    public interface IAlerter
    {
        public void SendAlert(string parameter, ParameterStatus status, BreachLevel level);
    }

    public class AlertByReportToCsvFile: IAlerter
    {
        private readonly CsvFileWriter _writer;

        public AlertByReportToCsvFile(string fileName)
        {
            _writer = new CsvFileWriter(fileName);
            List<string> columns = new List<string>() { "Date and Time", "Parameter", "Parameter Status", "Breach Level/ Alert Type" };
            _writer.WriteRow(columns);

        }

        ~AlertByReportToCsvFile()
        {
            _writer.Dispose();
        }

        public void SendAlert(string parameter, ParameterStatus status, BreachLevel level)
        {
            
            List<string> columns = new List<string>()
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