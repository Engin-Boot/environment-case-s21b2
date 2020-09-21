namespace AlertSystem
{
    public class MapGenerator
    {
        public static readonly MapRangeToParameterStatus[] TemperatureRangeMap;
        public static readonly MapRangeToParameterStatus[] HumidityRangeMap;

        static MapGenerator()
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
    }
}
