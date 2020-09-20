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
        public readonly int LowerLimit;
        public readonly int UpperLimit;
        public readonly ParameterStatus Status;
        public readonly BreachLevel Level;

        public MapRangeToParameterStatus(int lowerLimit, int upperLimit, ParameterStatus status, BreachLevel level)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Status = status;
            Level = level;
        }
    }
}