using System;
using System.Threading;

namespace PingOwin.Core.Interfaces
{
    public interface IPingOwinOptions
    {
        int WarnThreshold { get; set; }
        int RequestTimeOut { get; set; }
        string PathToDb { get; set; }
        Action<Action<CancellationToken>> StartService { get; set; }
        double PingIntervalInMillis { get; set; }
    }
}