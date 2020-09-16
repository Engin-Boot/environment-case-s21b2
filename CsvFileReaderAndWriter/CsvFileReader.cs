using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace CsvFileReaderAndWriter
{
    public class CsvFileReader : CsvFileCommon, IDisposable
    {
        // Private members
        private readonly StreamReader Reader;
        private string CurrLine;
        private int CurrPos;

        public CsvFileReader(Stream stream)
        {
            Reader = new StreamReader(stream);
            //EmptyLineBehavior = emptyLineBehavior;
        }


        public CsvFileReader(string path)
        {
            Reader = new StreamReader(path);
            //EmptyLineBehavior = emptyLineBehavior;
        }


        public bool ReadRow(List<string> columns)
        {
            // Verify required argument
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }

            //ReadNextLine:
            // Read next line from the file
            CurrLine = Reader.ReadLine();
            CurrPos = 0;
            // Test for end of file
            if (CurrLine == null)
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

            AddRowInColumn(columns);
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
            Debug.Assert(CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote);
            CurrPos++;

            // Parse column
            StringBuilder builder = new StringBuilder();
            ConseqQuotes(builder);

            if (CurrPos < CurrLine.Length)
            {
                // Consume closing quote
                Debug.Assert(CurrLine[CurrPos] == Quote);
                CurrPos++;
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

            int startPos = CurrPos;
            CurrPos = CurrLine.IndexOf(Delimiter, CurrPos);
            if (CurrPos == -1)
            {
                CurrPos = CurrLine.Length;
            }

            return CurrPos > startPos ? CurrLine.Substring(startPos, CurrPos - startPos) : string.Empty;
        }

        private void AddRowInColumn(List<string> columns)

        {
            // Parse line
            string column;
            int numColumns = 0;
            while (true)
            {
                // Read next column
                // if (CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote)
                //     column = ReadQuotedColumn();
                //   else
                //     column = ReadUnquotedColumn();
                column = FillStringcolumnValue();
                // Add column to list
                AddColumnToColumnsList(columns, numColumns, column);

                numColumns++;
                // Break if we reached the end of the line
                if (CurrPos == CurrLine.Length)
                {
                    break;
                }
                // Otherwise skip delimiter
                Debug.Assert(CurrLine[CurrPos] == Delimiter);
                CurrPos++;
            }
            // Remove any unused columns from collection
            RemoveUnusedColumnsFromColumnsList(columns, numColumns);
        }

        public string FillStringcolumnValue()
        {

            return CurrPos < CurrLine.Length && CurrLine[CurrPos] == Quote ? ReadQuotedColumn() : ReadUnquotedColumn();
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
            if (CurrLine[CurrPos] == Quote)
            {

                if (CheckForTwoQuotesIf())
                {
                    CurrPos++;
                }
                else
                {
                    //break;  // Single quote ends quoted sequence
                    return;
                }
            }
            // Add current character to the column
            _ = builder.Append(CurrLine[CurrPos++]);
            ConseqQuotes(builder);
            //}
        }

        public bool CheckForTwoQuotesIf()
        {
            // If two quotes, skip first and treat second as literal
            int nextPos = CurrPos + 1;
            bool val = nextPos < CurrLine.Length && CurrLine[nextPos] == Quote;
            return val;
        }
        public void EndOfLineConditionCheckInConseqQuotes(StringBuilder builder)
        {
            while (CurrPos == CurrLine.Length)
            {
                // End of line so attempt to read the next line
                CurrLine = Reader.ReadLine();
                CurrPos = 0;
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
            Reader.Dispose();
        }
    }

}
