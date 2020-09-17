﻿namespace AlertSystem
{
    public class RangeChecker
    {
        private readonly string _parameter; 
        private readonly MapRangeToParameterStatus[] _map;

        public RangeChecker(string parameter, MapRangeToParameterStatus[] map)
        {
            _parameter = parameter;
            _map = new MapRangeToParameterStatus[map.Length];
            for (int i = 0; i < map.Length; i++)
            {
                _map[i] = new MapRangeToParameterStatus(
                    map[i].LowerLimit, 
                    map[i].UpperLimit,
                    map[i].Status,
                    map[i].Level);
            }
        }

        public RangeResult CalculateParameterRangeResult(int parameterValue)
        {
            RangeResult result = new RangeResult
            {
                Parameter = this._parameter,
                Status = ParameterStatus.Normal,
                Level = BreachLevel.Safe
            };

            for (int i = 0; i < _map.Length; i++)
            {
                if (IsParameterInRange(_map[i].LowerLimit, _map[i].UpperLimit, parameterValue))
                {
                    result.Status = _map[i].Status;
                    result.Level = _map[i].Level;
                    break;
                }
            }

            return result;
        }

        private bool IsParameterInRange(int lower, int upper, int value)
        {
            return value >= lower && value <= upper;
        }
    }
}