namespace AlertSystem
{
    class Program
    {
        private static readonly MapRangeToParameterStatus[] TemperatureRangeMap;

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

        }
        static void Main()
        {
            
        }
    }
}
