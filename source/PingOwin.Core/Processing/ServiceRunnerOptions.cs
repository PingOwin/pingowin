using System;
using System.Threading;

namespace PingOwin.Core.Processing
{
    public class ServiceRunnerOptions
    {
        public long PingIntervalInMillis { get; set; }
        public Action<Action<CancellationToken>> RunBackgroundThread { get; set; }
    }
}
