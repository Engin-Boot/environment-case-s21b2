namespace AlertSystem
{
    public enum BreachLevel
    {
        Safe,
        Warning,
        Error
    }
    public struct RangeResult
    {
        public string Parameter;
        public ParameterStatus Status;
        public BreachLevel Level;
    }
}