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
        private StreamWriter Writer;
        private string OneQuote = null;
        private string TwoQuotes = null;
        private string QuotedFormat = null;

        /// <summary>
        /// Initializes a new instance of the CsvFileWriter class for the
        /// specified file path.
        /// </summary>
        /// <param name="path">The name of the CSV file to write to</param>
        public CsvFileWriter(string path)
        {
            Writer = new StreamWriter(path);
        }

        /// <summary>
        /// Writes a row of columns to the current CSV file.
        /// </summary>
        /// <param name="columns">The list of columns to write</param>
        public void WriteRow(List<string> columns)
        {
            // Verify required argument
            VerifyColumns(columns);
            
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
            Writer.WriteLine();
        }

        /// <summary>
        /// Verify columns list is not null otherwise throw exception
        /// </summary>
        /// <param name="columns">The list of columns to write</param>
        private void VerifyColumns(List<string> columns)
        {
            if (columns == null)
                throw new ArgumentNullException("columns");
        }

        // Set values of quotes as per current quote character
        private void SetupQuotes()
        {
            if (OneQuote == null || OneQuote[0] != Quote)
            {
                OneQuote = String.Format("{0}", Quote);
                TwoQuotes = String.Format("{0}{0}", Quote);
                QuotedFormat = String.Format("{0}{{0}}{0}", Quote);
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
                Writer.Write(Delimiter);
        }

        /// <summary>
        /// Add column content to CSV file. 
        /// The column content is wrapped in quotes if it contains a special character
        /// </summary>
        /// <param name="column">The string or content of a column</param>
        private void WriteColumn(string column)
        {
            if (column.IndexOfAny(SpecialChars) == -1)
                Writer.Write(column);
            else
                Writer.Write(QuotedFormat, column.Replace(OneQuote, TwoQuotes));
        }

        // Propagate Dispose to StreamWriter
        public void Dispose()
        {
            Writer.Dispose();
        }
    }
}
