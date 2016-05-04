using System;

namespace PingOwin.Core
{
    public interface IPingConfiguration
    {
        TimeSpan RequestTimeOut { get; }
        long WarnThreshold   { get; }
    }
}