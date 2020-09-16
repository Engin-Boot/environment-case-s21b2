using System;
using System.Collections.Generic;
using Xunit;

namespace CsvFileReaderAndWriter.Test
{
    public class CsvFileWriterUnitTest
    {
        [Fact]
        public void WhenValidColumnsListWithoutSpecialCharacterThenWriteRowToCsv()
        {
            using var writer = new CsvFileWriter("SampleWriteTest.csv");
            // Write each row of data
            for (int row = 0; row < 100; row++)
            {
                List<string> columns = new List<string>() { "Date Time", "Alert Level", "Alert Message" };
                writer.WriteRow(columns);
            }
        }

        [Fact]
        public void WhenValidColumnsListWithSpecialCharacterThenWriteRowToCsvWithQuotedFormat()
        {
            using var writer = new CsvFileWriter("SampleWriteTest.csv");
            // Write each row of data
            for (int row = 0; row < 100; row++)
            {
                List<string> columns = new List<string>() { "Date \n Time", "Alert Level", "Alert Message" };
                writer.WriteRow(columns);
            }
        }

        [Fact]
        public void WhenNullColumnsListThenThrowArgumentNullException()
        {
            using var writer = new CsvFileWriter("SampleWriteTest.csv");

            Exception ex = Assert.Throws<ArgumentNullException>(() => writer.WriteRow(null));

            Assert.Equal("Value cannot be null. (Parameter 'columns')", ex.Message);
        }
    }
}
