using System;

namespace PingOwin.Core.Frontend
{
    public class PingConfiguration : IPingConfiguration
    {
        public TimeSpan RequestTimeOut { get; set; }

        public long WarnThreshold { get; set; }
    }
}