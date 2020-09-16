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
        public string Message;

        public MapRangeToParameterStatus(int lowerLimit, int upperLimit, ParameterStatus status, string message)
        {
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            Status = status;
            Message = message;
        }
    }
}