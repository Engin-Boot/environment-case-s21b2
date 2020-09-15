using System.Collections.Generic;
using Xunit;


namespace CsvFileReaderAndWriter.Test
{
    public class CsvFileWriterTest
    {
        [Fact]
        public void SampleTest()
        {
            WriteValues();
        }
        private void WriteValues()
        {
            using (var writer = new CsvFileWriter("SampleWriteTest.csv"))
            {
                // Write each row of data
                for (int row = 0; row < 100; row++)
                {
                    List<string> columns = new List<string>(){"Date Time", "Alert Level", "Alert Message"};
                    writer.WriteRow(columns);
                }
            }
        }
    }
}
