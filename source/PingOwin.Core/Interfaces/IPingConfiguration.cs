using System;

namespace PingOwin.Core.Interfaces
{
    public interface IPingConfiguration
    {
        TimeSpan RequestTimeOut { get; }
        long WarnThreshold   { get; }
    }
}