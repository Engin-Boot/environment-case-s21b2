using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace MonitoringSensor
{
    /// <summary>
    /// Class for writing to comma-separated-value (CSV) files.
    /// </summary>
    public class CsvFileReader : CsvFileCommon, IDisposable
    {
        // Private members
        private readonly StreamReader _reader;
        private string _currentLine;
        private int _currentPosition;

        /// <summary>
        /// Initializes a new instance of the CsvFileReader class for the specified stream.
        /// </summary>
        public CsvFileReader(Stream stream)
        {
            _reader = new StreamReader(stream);
        }

        /// <summary>
        /// Initializes a new instance of the CsvFileReader class for the specified file path.
        /// </summary>
        public CsvFileReader(string path)
        {
            _reader = new StreamReader(path);
        }


        /// <summary>
        /// Reads a row of columns from the current CSV file. Returns false if no more data could be read because the end of the file was reached.
        /// </summary>
        /// <param name="columns">Collection to hold the columns read</param>
        public bool ReadRow(List<string> columns)
        {

            // Read next line from the file
            _currentLine = _reader.ReadLine();
            _currentPosition = 0;

            // Test for end of file
            if (_currentLine == null)
            {
                return false;
            }

            AddRowInColumn(columns);

            // Indicate success
            return true;
        }

        /// <summary>
        /// Reads a column by reading from the current line until a delimiter is found or the end of the line is reached. On return, the
        /// current position points to the delimiter or the end of the current line.
        /// </summary>
        private string ReadColumn()
        {

            var startPos = _currentPosition;
            _currentPosition = _currentLine.IndexOf(Delimiter, _currentPosition);
            if (_currentPosition == -1)
            {
                _currentPosition = _currentLine.Length;
            }

            return _currentPosition > startPos ? _currentLine.Substring(startPos, _currentPosition - startPos) : string.Empty;
        }

        private void AddRowInColumn(List<string> columns)
        {
            // Parse line
            var numColumns = 0;
            while (true)
            {
                // Read next column
                var column = ReadColumn();
                // Add column to list
                AddColumnToColumnsList(columns, numColumns, column);

                numColumns++;
                // Break if we reached the end of the line
                if (_currentPosition == _currentLine.Length)
                {
                    break;
                }
                // Otherwise skip delimiter
                Debug.Assert(_currentLine[_currentPosition] == Delimiter);
                _currentPosition++;
            }
            // Remove any unused columns from collection
            RemoveUnusedColumnsFromColumnsList(columns, numColumns);
        }

        private static void RemoveUnusedColumnsFromColumnsList(List<string> columns, int numColumns)
        {
            if (numColumns < columns.Count)
            {
                columns.RemoveRange(numColumns, columns.Count - numColumns);
            }
        }

        private static void AddColumnToColumnsList(IList<string> columns, int numColumns, string column)
        {
            if (numColumns < columns.Count)
            {
                columns[numColumns] = column;
            }
            else
            {
                columns.Add(column);
            }
        }

        // Propagate Dispose to StreamReader
        public void Dispose()
        {
            _reader.Dispose();
        }
    }

}
