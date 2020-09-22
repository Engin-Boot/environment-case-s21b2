
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using MonitoringSensor;
using Xunit;

namespace CsvFileReaderAndWriter.Test
{
    public class CsvFileReaderUnitTest
    {
        private bool IsReadRowPossibleForGivenInput(string testInput, List<string> columns)
        {
            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(testInput));
            using var reader = new CsvReader(stream);
            var result = reader.ReadRow(columns);

            return result;
        }
        [Fact]
        public void WhenValidColumnsListIsEmptyThenReturnFalseToShowEndOfFile()
        {
            var testInput = string.Empty;
            var columns = new List<string>();
            bool result = IsReadRowPossibleForGivenInput(testInput, columns);
            Debug.Assert(columns.Count == 0);
            Assert.False(result);
        }

        [Fact]
        public void WhenValidFileInputThenReadRowToCsv()
        {

            const string testInput = "123,456,789,0";
            var columns = new List<string>();
            bool result = IsReadRowPossibleForGivenInput(testInput, columns);
            Assert.True(result);
            Debug.Assert(columns.Count == 4);
            Debug.Assert(columns[0] == "123");
            Debug.Assert(columns[1] == "456");
            Debug.Assert(columns[2] == "789");
            Debug.Assert(columns[3] == "0");
        }

        
    }
}
