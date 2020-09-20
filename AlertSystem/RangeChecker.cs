namespace AlertSystem
{
    public delegate void ParameterRangeBreachHandler(string parameter, ParameterStatus status, BreachLevel level);
    public class RangeChecker
    {

#region Events
        private ParameterRangeBreachHandler _parameterRangeBreached;
#endregion

#region Event Mutators

        private void Add_ParameterRangeBreached(ParameterRangeBreachHandler handler)
        {
            _parameterRangeBreached += handler;
        }
#endregion

        private readonly string _parameter; 
        private readonly MapRangeToParameterStatus[] _map;

        public RangeChecker(string parameter, MapRangeToParameterStatus[] map, ParameterRangeBreachHandler handler)
        {
            _parameter = parameter;
            _map = new MapRangeToParameterStatus[map.Length];
            for (var i = 0; i < map.Length; i++)
            {
                _map[i] = new MapRangeToParameterStatus(
                    map[i].LowerLimit, 
                    map[i].UpperLimit,
                    map[i].Status,
                    map[i].Level);
            }

            Add_ParameterRangeBreached(handler);
        }

        public RangeResult CalculateParameterRangeResult(int parameterValue)
        {
            var result = new RangeResult
            {
                Parameter = _parameter,
                Status = ParameterStatus.Normal,
                Level = BreachLevel.Safe
            };

            for (var i = 0; i < _map.Length; i++)
            {
                if (!IsParameterInRange(_map[i].LowerLimit, _map[i].UpperLimit, parameterValue))
                    continue;
                
                result.Status = _map[i].Status;
                result.Level = _map[i].Level;
                OnParameterRangeBreach(result);
                break;
            }

            return result;
        }

        private void OnParameterRangeBreach(RangeResult result)
        {
            _parameterRangeBreached?.Invoke(_parameter, result.Status, result.Level);
        }

        private static bool IsParameterInRange(int lower, int upper, int value)
        {
            return value >= lower && value <= upper;
        }
    }
}