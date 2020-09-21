namespace AlertSystem
{
    public enum ParameterStatus
    {
        VeryLow,
        Low,
        Normal,
        High,
        VeryHigh
    }
    public readonly struct MapRangeToParameterStatus
    {
        public readonly double LowerLimit;
        public readonly double UpperLimit;
        public readonly ParameterStatus Status;
        public readonly BreachLevel Level;

        public MapRangeToParameterStatus(double lowerLimit, double upperLimit, ParameterStatus status, BreachLevel level)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Status = status;
            Level = level;
        }
    }
}