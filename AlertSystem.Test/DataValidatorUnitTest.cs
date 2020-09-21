using System;
using Xunit;

namespace AlertSystem.Test
{
    public class DataValidatorUnitTest
    {
        [Fact]
        public void WhenInputStringMatchesRegexThenValidateToTrue()
        {
            var result = DataValidator.IsValidInput("25C 44%");
            Assert.True(result);
        }

        [Fact]
        public void WhenInputStringDoesNotMatchRegexThenValidateToFalse()
        {
            var result = DataValidator.IsValidInput("25C");
            Assert.False(result);
        }

        [Fact]
        public void WhenTemperatureWithUnitStringIsParsedThenReturnTemperatureValue()
        {
            const string temperatureWithUnit = "25C";
            var temperatureValue = DataValidator.ParameterParser(temperatureWithUnit);
            Assert.Equal(25, temperatureValue);
        }

        [Fact]
        public void WhenInputStringInIncorrectFormatIsParsedThenThrowFormatException()
        {
            const string invalidString = "abC";
            Exception ex = Assert.Throws<FormatException>(() => DataValidator.ParameterParser(invalidString));
            Assert.Equal("Input string was not in a correct format.", ex.Message);
        }

        [Fact]
        public void WhenEmptyInputStringIsParsedThenThrowArgumentOutOfRangeException()
        {
            const string emptyString = "";
            Exception ex = Assert.Throws<ArgumentOutOfRangeException>(() => DataValidator.ParameterParser(emptyString));
            Assert.Equal("Length cannot be less than zero. (Parameter 'length')", ex.Message);
        }
    }
}
