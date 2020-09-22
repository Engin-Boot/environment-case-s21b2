namespace CsvFileReaderAndWriter
{
    /// <summary>
    /// Common base class for CSV reader and writer classes.
    /// </summary>
    public abstract class CsvFileCommon
    {
        /// <summary>
        /// These are special characters in CSV files. If a column contains any
        /// of these characters, the entire column is wrapped in double quotes.
        /// </summary>
        private readonly char[] _specialChars = { ',', '"', '\r', '\n' };

        // Indexes into SpecialChars for characters with specific meaning
        private const int DelimiterIndex = 0;
        private const int QuoteIndex = 1;

        /// <summary>
        /// Gets/sets the character used for column delimiters.
        /// </summary>
        protected char Delimiter => _specialChars[DelimiterIndex];

        /// <summary>
        /// Gets/sets the character used for column quotes.
        /// </summary>
        protected char Quote => _specialChars[QuoteIndex];
    }
}
