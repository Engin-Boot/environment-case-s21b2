using System;

namespace AlertSystem
{
    public interface IAlerter:IDisposable
    {
        public void SendAlert(string parameter, ParameterStatus status, BreachLevel level);
    }
}