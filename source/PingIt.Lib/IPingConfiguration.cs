using System;

namespace PingIt.Lib
{
    public interface IPingConfiguration
    {
        TimeSpan RequestTimeOut { get; }
        long WarnThreshold   { get; }
    }
}