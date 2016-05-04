using System;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Processing
{
    public class PingConfiguration : IPingConfiguration
    {
        public TimeSpan RequestTimeOut { get; set; }

        public long WarnThreshold { get; set; }
    }
}