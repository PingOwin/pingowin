using System;
using System.Threading;

namespace PingOwin.Core.Frontend
{
    public class PingOwinOptions
    {
        public PingOwinOptions()
        {
            PathToDb = AppDomain.CurrentDomain.BaseDirectory;
            PingIntervalInMillis = 5000;
            RequestTimeOut = 10000;
            WarnThreshold = 1000;
        }

        public int WarnThreshold { get; set; }

        public int RequestTimeOut { get; set; }

        public string PathToDb { get; set; }
        public Action<Action<CancellationToken>> StartService { get; set; }
        public long PingIntervalInMillis { get; set; }
        
    }
}