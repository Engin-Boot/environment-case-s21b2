using System;
using System.Text.RegularExpressions;

namespace AlertSystem
{
    internal class Program
    {
        private static readonly MapRangeToParameterStatus[] TemperatureRangeMap;
        private static readonly MapRangeToParameterStatus[] HumidityRangeMap;

        static Program()
        {
            TemperatureRangeMap = new MapRangeToParameterStatus[4];

            TemperatureRangeMap[0] =
                new MapRangeToParameterStatus(int.MinValue, -1, ParameterStatus.VeryLow, BreachLevel.Error);

            TemperatureRangeMap[1] =
                new MapRangeToParameterStatus(0, 3, ParameterStatus.Low, BreachLevel.Warning);

            TemperatureRangeMap[2] =
                new MapRangeToParameterStatus(38, 40, ParameterStatus.High, BreachLevel.Warning);

            TemperatureRangeMap[3] =
                new MapRangeToParameterStatus(41, int.MaxValue, ParameterStatus.VeryHigh, BreachLevel.Error);

            HumidityRangeMap = new MapRangeToParameterStatus[2];

            HumidityRangeMap[0] = 
                new MapRangeToParameterStatus(71, 90, ParameterStatus.High, BreachLevel.Warning);

            HumidityRangeMap[1] = 
                new MapRangeToParameterStatus(91, 100, ParameterStatus.VeryHigh, BreachLevel.Error);

        }

        private static int ParameterParser(string data)
        {
            int value;
            try
            {
                value = int.Parse(data.Trim().Substring(0, data.Length - 1));
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e);
                throw;
            }

            return value;
        }

        private static bool ReadInput(out string data)
        {
            Console.WriteLine("Enter temperature and humidity (exit to break)");
            data = Console.ReadLine();
            return data != null && data != "exit";
        }

        private static bool IsValidInput(string data)
        {
            const string spacePattern = @"\s*";
            const string acceptedNumberPattern = @"[\+-]?(\d*\.?\d?)";
            const string temperatureUnitPattern = @"[ckf]";
            const string humidityUnitPattern = @"[%]";

            var regex = new Regex(
                acceptedNumberPattern +
                temperatureUnitPattern +
                spacePattern +
                acceptedNumberPattern +
                humidityUnitPattern, RegexOptions.IgnoreCase);

            return regex.IsMatch(data.Trim());
        }

        public static void Main()
        {

            var csvFileAlerter = new AlertByReportToCsvFile("RangeCheckerTestFile.csv");

            var temperatureRangeChecker = new RangeChecker("Temperature", TemperatureRangeMap, csvFileAlerter.SendAlert);
            var humidityRangeChecker = new RangeChecker("Humidity", HumidityRangeMap, csvFileAlerter.SendAlert);

            while (ReadInput(out var data))
            {
                if (!IsValidInput(data)) continue;
                var parameters = data.Split(" ");

                var temperature = ParameterParser(parameters[0]);

                var humidity = ParameterParser(parameters[1]);

                temperatureRangeChecker.CalculateParameterRangeResult(temperature);

                humidityRangeChecker.CalculateParameterRangeResult(humidity);
            }

            csvFileAlerter.Dispose();
        }
    }
}
