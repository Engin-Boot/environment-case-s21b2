using System;
using System.Collections.Generic;
using System.IO;

namespace CsvFileReaderAndWriter
{
    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class CsvFileWriter : CsvFileCommon, IDisposable
    {
        // Private members
        private readonly StreamWriter _writer;
        private string _oneQuote;
        private string _twoQuotes;
        private string _quotedFormat;

        /// <summary>
        /// Initializes a new instance of the CsvFileWriter class for the
        /// specified file path.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to</param>
        public CsvFileWriter(string path)
        {
            _writer = new StreamWriter(path);
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write</param>
        public void WriteRow(List<string> columns)
        {
            // Verify required argument
            if (columns == null) throw new ArgumentNullException(nameof(columns));

            // Ensure we're using current quote character
            SetupQuotes();

            // Write each column
            for (int i = 0; i < columns.Count; i++)
            {
                // Add delimiter
                AddDelimiter(i);

                // Write this column
                WriteColumn(columns[i]);
            }
            _writer.WriteLine();
        }

        // Set values of quotes as per current quote character
        private void SetupQuotes()
        {
            if (_oneQuote == null || _oneQuote[0] != Quote)
            {
                _oneQuote = $"{Quote}";
                _twoQuotes = String.Format("{0}{0}", Quote);
                _quotedFormat = String.Format("{0}{{0}}{0}", Quote);
            }
        }

        /// <summary>
        /// Add delimiter to CSV file based on column index
        /// </summary>
        /// <param name="columnIndex">The index of column</param>
        private void AddDelimiter(int columnIndex)
        {
            // Add delimiter if this isn't the first column of a row
            if (columnIndex > 0)
                _writer.Write(Delimiter);
        }

        /// <summary>
        /// Add column content to CSV file. 
        /// The column content is wrapped in quotes if it contains a special character
        /// </summary>
        /// <param name="column">The string or content of a column</param>
        private void WriteColumn(string column)
        {
            if (column.IndexOfAny(SpecialChars) == -1)
                _writer.Write(column);
            else
                _writer.Write(_quotedFormat, column.Replace(_oneQuote, _twoQuotes));
        }

        // Propagate Dispose to StreamWriter
        public void Dispose()
        {
            _writer.Dispose();
        }
    }
}
