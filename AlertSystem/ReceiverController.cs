using System;
using System.Diagnostics.CodeAnalysis;

namespace AlertSystem
{
    public class ReceiverController
    {
        [ExcludeFromCodeCoverage]
        private static bool ReadInput(out string data)
        {
            data = Console.ReadLine();
            return data != null && data != "exit";
        }

        [ExcludeFromCodeCoverage]
        public static void Main()
        {
            var csvFileAlerter = new AlertByReportToCsvFile("RangeCheckerTestFile.csv");

            var temperatureRangeChecker = new RangeChecker("Temperature", MapGenerator.TemperatureRangeMap, csvFileAlerter.SendAlert);
            var humidityRangeChecker = new RangeChecker("Humidity", MapGenerator.HumidityRangeMap, csvFileAlerter.SendAlert);

            while (ReadInput(out var data))
            {
                if (!DataValidator.IsValidInput(data)) continue;
                var parameters = data.Split(" ");

                try
                {
                    var temperature = DataValidator.ParameterParser(parameters[0]);

                    var humidity = DataValidator.ParameterParser(parameters[1]);

                    temperatureRangeChecker.CalculateParameterRangeResult(temperature);

                    humidityRangeChecker.CalculateParameterRangeResult(humidity);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine(ex);
                }

                catch (FormatException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            csvFileAlerter.Dispose();
        }
    }
}
