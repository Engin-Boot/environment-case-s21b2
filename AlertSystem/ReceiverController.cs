using System;
using System.Diagnostics.CodeAnalysis;

namespace AlertSystem
{
    public static class ReceiverController
    {
        [ExcludeFromCodeCoverage]
        private static bool ReadInput(out string data)
        {
            data = Console.ReadLine();
            return DataValidator.IsValidInput(data);
        }

        [ExcludeFromCodeCoverage]
        public static void Main()
        {
            var csvFileAlerter = new AlertByReportToCsvFile("RangeCheckerTestFile.csv");

            var temperatureRangeChecker = new RangeChecker("Temperature", MapGenerator.TemperatureRangeMap, csvFileAlerter.SendAlert);
            var humidityRangeChecker = new RangeChecker("Humidity", MapGenerator.HumidityRangeMap, csvFileAlerter.SendAlert);

            while (ReadInput(out var data))
            {
                try
                {
                    var parameters = data.Split(" ");

                    var temperature = DataValidator.ParameterParser(parameters[0]);

                    var humidity = DataValidator.ParameterParser(parameters[1]);

                    temperatureRangeChecker.CalculateParameterRangeResult(temperature);

                    humidityRangeChecker.CalculateParameterRangeResult(humidity);
                }
                catch (SystemException ex)
                {
                    Console.WriteLine(ex);
                }
            }

            csvFileAlerter.Dispose();
        }
    }
}
