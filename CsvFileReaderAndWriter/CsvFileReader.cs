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
        private string _currLine;
        private int _currPos;

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
            _currLine = _reader.ReadLine();
            _currPos = 0;
            // Test for end of file
            if (_currLine == null)
            {
                return false;
            }
            // Test for empty line
            //if (CurrLine.Length == 0)
            //{
            //Console.WriteLine("yoyo");
            //goto ReadNextLine;
            //    columns.Clear();
            //    return true;
            //}

            AddRowInColumn(columns: columns);
            // Indicate success
            return true;
        }

        /// <summary>
        /// Reads a quoted column by reading from the current line until a
        /// closing quote is found or the end of the file is reached. On return,
        /// the current position points to the delimiter or the end of the last
        /// line in the file. Note: CurrLine may be set to null on return.
        /// </summary>
        private string ReadQuotedColumn()
        {
            // Skip opening quote character
            Debug.Assert(_currPos < _currLine.Length && _currLine[_currPos] == Quote);
            _currPos++;

            // Parse column
            StringBuilder builder = new StringBuilder();
            ConseqQuotes(builder);

            if (_currPos < _currLine.Length)
            {
                // Consume closing quote
                Debug.Assert(_currLine[_currPos] == Quote);
                _currPos++;
                // Append any additional characters appearing before next delimiter
                builder.Append(ReadUnquotedColumn());
            }
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

            int startPos = _currPos;
            _currPos = _currLine.IndexOf(Delimiter, _currPos);
            if (_currPos == -1)
            {
                _currPos = _currLine.Length;
            }

            return _currPos > startPos ? _currLine.Substring(startPos, _currPos - startPos) : string.Empty;
        }

        private void AddRowInColumn(List<string> columns)

        {
            // Parse line
            int numColumns = 0;
            while (true)
            {
                // Read next column
                // if (CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote)
                //     column = ReadQuotedColumn();
                //   else
                //     column = ReadUnquotedColumn();
                var column = FillStringcolumnValue();
                // Add column to list
                AddColumnToColumnsList(columns, numColumns, column);

                numColumns++;
                // Break if we reached the end of the line
                if (_currPos == _currLine.Length)
                {
                    break;
                }
                // Otherwise skip delimiter
                Debug.Assert(_currLine[_currPos] == Delimiter);
                _currPos++;
            }
            // Remove any unused columns from collection
            RemoveUnusedColumnsFromColumnsList(columns, numColumns);
        }

        public string FillStringcolumnValue()
        {

            return _currPos < _currLine.Length && _currLine[_currPos] == Quote ? ReadQuotedColumn() : ReadUnquotedColumn();
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

        public void ConseqQuotes(StringBuilder builder)
        {
            // while (true)
            // {

            EndOfLineConditionCheckInConseqQuotes(builder);

            // Test for quote character
            if (_currLine[_currPos] == Quote)
            {

                if (CheckForTwoQuotesIf())
                {
                    _currPos++;
                }
                else
                {
                    //break;  // Single quote ends quoted sequence
                    return;
                }
            }
            // Add current character to the column
            _ = builder.Append(_currLine[_currPos++]);
            ConseqQuotes(builder);
            //}
        }

        public bool CheckForTwoQuotesIf()
        {
            // If two quotes, skip first and treat second as literal
            int nextPos = _currPos + 1;
            bool val = nextPos < _currLine.Length && _currLine[nextPos] == Quote;
            return val;
        }
        public void EndOfLineConditionCheckInConseqQuotes(StringBuilder builder)
        {
            while (_currLine != null && _currPos == _currLine.Length)
            {
                // End of line so attempt to read the next line
                _currLine = _reader.ReadLine();
                _currPos = 0;
                // Done if we reached the end of the file
                //  if (CurrLine == null)
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
