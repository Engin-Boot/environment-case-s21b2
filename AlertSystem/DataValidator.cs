using System;
using System.Text.RegularExpressions;

namespace AlertSystem
{
    public static class DataValidator
    {
        public static double ParameterParser(string data)
        {
            double value;
            try
            {
                value = double.Parse(data.Trim().Substring(0, data.Length - 1));
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            catch (FormatException ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return value;
        }
        public static bool IsValidInput(string data)
        {
            const string spacePattern = @"\s*";
            const string acceptedNumberPattern = @"[\+-]?(\d*\.?\d?\d?)";
            const string temperatureUnitPattern = @"[ckf]";
            const string humidityUnitPattern = @"[%]";

            var regex = new Regex(
                acceptedNumberPattern +
                temperatureUnitPattern +
                spacePattern +
                acceptedNumberPattern +
                humidityUnitPattern, RegexOptions.IgnoreCase);

            return data != null && data != "exit" && regex.IsMatch(data.Trim());
        }
    }
}
