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
    public struct MapRangeToParameterStatus
    {
        public int LowerLimit;
        public int UpperLimit;
        public ParameterStatus Status;
        public BreachLevel Level;

        public MapRangeToParameterStatus(int lowerLimit, int upperLimit, ParameterStatus status, BreachLevel level)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Status = status;
            Level = level;
        }
    }
}