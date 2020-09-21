namespace AlertSystem
{
    public static class MapGenerator
    {
        public static readonly MapRangeToParameterStatus[] TemperatureRangeMap;
        public static readonly MapRangeToParameterStatus[] HumidityRangeMap;

        static MapGenerator()
        {
            TemperatureRangeMap = new MapRangeToParameterStatus[4];

            TemperatureRangeMap[0] =
                new MapRangeToParameterStatus(double.MinValue, -0.01, ParameterStatus.VeryLow, BreachLevel.Error);

            TemperatureRangeMap[1] =
                new MapRangeToParameterStatus(0.00, 3.99, ParameterStatus.Low, BreachLevel.Warning);

            TemperatureRangeMap[2] =
                new MapRangeToParameterStatus(37.01, 40.00, ParameterStatus.High, BreachLevel.Warning);

            TemperatureRangeMap[3] =
                new MapRangeToParameterStatus(40.01, double.MaxValue, ParameterStatus.VeryHigh, BreachLevel.Error);

            HumidityRangeMap = new MapRangeToParameterStatus[2];

            HumidityRangeMap[0] =
                new MapRangeToParameterStatus(70.01, 90.00, ParameterStatus.High, BreachLevel.Warning);

            HumidityRangeMap[1] =
                new MapRangeToParameterStatus(90.01, 100.00, ParameterStatus.VeryHigh, BreachLevel.Error);

        }
    }
}
