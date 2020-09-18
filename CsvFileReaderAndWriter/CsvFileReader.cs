using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace CsvFileReaderAndWriter
{
    public class CsvFileReader : CsvFileCommon, IDisposable
    {
        // Private members
        private readonly StreamReader _reader;
        private string _currentLine;
        private int _currentPosition;

/*
        public CsvFileReader(Stream stream)
        {
            _reader = new StreamReader(stream);
            //EmptyLineBehavior = emptyLineBehavior;
        }
*/


        public CsvFileReader(string path)
        {
            _reader = new StreamReader(path);
            //EmptyLineBehavior = emptyLineBehavior;
        }


        public bool ReadRow(List<string> columns)
        {
            // Verify required argument
            if (columns == null)
            {
                throw new ArgumentNullException(nameof(columns));
            }

            //ReadNextLine:
            // Read next line from the file
            _currentLine = _reader.ReadLine();
            _currentPosition = 0;
            // Test for end of file
            if (_currentLine == null)
            {
                return false;
            }
            // Test for empty line
            //if (_currentLine.Length == 0)
            //{
            
            //goto ReadNextLine;
            //    columns.Clear();
            //    return true;
            //}

            AddRowInColumn(columns);
            // Indicate success
            return true;
        }

        /// <summary>
        /// Reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: CurrentLine may be set to null on return.
        /// </summary>
        private string ReadQuotedColumn()
        {
            // Skip opening quote character
            Debug.Assert(_currentPosition < _currentLine.Length && _currentLine[_currentPosition] == Quote);
            _currentPosition++;

            // Parse column
            StringBuilder builder = new StringBuilder();
            ConsecutiveQuotes(builder);

            if (_currentPosition >= _currentLine.Length) return builder.ToString();
            // Consume closing quote
            Debug.Assert(_currentLine[_currentPosition] == Quote);
            _currentPosition++;
            // Append any additional characters appearing before next delimiter
            builder.Append(ReadUnquotedColumn());
            // Return column value
            return builder.ToString();
        }

        /// <summary>
        /// Reads an unquoted column by reading from the current line until a
        /// delimiter is found or the end of the line is reached. On return, the
        /// current position points to the delimiter or the end of the current
        /// line.
        /// </summary>
        private string ReadUnquotedColumn()
        {

            int startPos = _currentPosition;
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
            int numColumns = 0;
            while (true)
            {
                // Read next column
                // if (_currentPosition < _currentLine.Length && _currentLine[_currentPos] == Quote)
                //     column = ReadQuotedColumn();
                //   else
                //     column = ReadUnquotedColumn();
                var column = FillStringColumnValue();
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

        public string FillStringColumnValue()
        {

            return _currentPosition < _currentLine.Length && _currentLine[_currentPosition] == Quote ? ReadQuotedColumn() : ReadUnquotedColumn();
        }
        public void RemoveUnusedColumnsFromColumnsList(List<string> columns, int numColumns)
        {
            if (numColumns < columns.Count)
            {
                columns.RemoveRange(numColumns, columns.Count - numColumns);
            }
        }
        public void AddColumnToColumnsList(List<string> columns, int numColumns, string column)
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

        public void ConsecutiveQuotes(StringBuilder builder)
        {
            // while (true)
            // {

            EndOfLineConditionCheckInConsecutiveQuotes(builder);

            // Test for quote character
            if (_currentLine[_currentPosition] == Quote)
            {

                if (CheckForTwoQuotesIf())
                {
                    _currentPosition++;
                }
                else
                {
                    //break;  // Single quote ends quoted sequence
                    return;
                }
            }
            // Add current character to the column
            _ = builder.Append(_currentLine[_currentPosition++]);
            ConsecutiveQuotes(builder);
            //}
        }

        public bool CheckForTwoQuotesIf()
        {
            // If two quotes, skip first and treat second as literal
            int nextPos = _currentPosition + 1;
            bool val = nextPos < _currentLine.Length && _currentLine[nextPos] == Quote;
            return val;
        }
        public void EndOfLineConditionCheckInConsecutiveQuotes(StringBuilder builder)
        {
            while (_currentLine != null && _currentPosition == _currentLine.Length)
            {
                // End of line so attempt to read the next line
                _currentLine = _reader.ReadLine();
                _currentPosition = 0;
                // Done if we reached the end of the file
                //  if (_currentLine == null)
                //      return builder.ToString();
                // Otherwise, treat as a multi-line field
                _ = builder.Append(Environment.NewLine);
            }
        }
        // Propagate Dispose to StreamReader
        public void Dispose()
        {
            _reader.Dispose();
        }
    }

}
