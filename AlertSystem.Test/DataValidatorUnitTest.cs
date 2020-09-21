using System;
using Xunit;

namespace AlertSystem.Test
{
    public class DataValidatorUnitTest
    {
        [Fact]
        public void WhenInputStringMatchesRegexThenValidateToTrue()
        {
            var result = DataValidator.IsValidInput("25.22C 44.02%");
            Assert.True(result);

            result = DataValidator.IsValidInput("25C 44.5%");
            Assert.True(result);
        }

        [Fact]
        public void WhenInputStringDoesNotMatchRegexThenValidateToFalse()
        {
            var result = DataValidator.IsValidInput("25.15C");
            Assert.False(result);
        }

        [Fact]
        public void WhenTemperatureWithUnitStringIsParsedThenReturnTemperatureValue()
        {
            const string temperatureWithUnit = "25.00C";
            var temperatureValue = DataValidator.ParameterParser(temperatureWithUnit);
            Assert.Equal(25.00, temperatureValue);

            const string humidityWithUnit = "25%";
            var humidityValue = DataValidator.ParameterParser(humidityWithUnit);
            Assert.Equal(25, humidityValue);
            Assert.Equal(typeof(double), humidityValue.GetType());
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
