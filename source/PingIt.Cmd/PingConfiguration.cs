using System;
using System.Configuration;
using PingIt.Lib;

namespace PingIt.Cmd
{
    public class PingConfiguration : IPingConfiguration
    {
        public TimeSpan RequestTimeOut
        {
            get
            {
                var milliseconds = int.Parse(ConfigurationManager.AppSettings["timeoutInMs"]);
                return new TimeSpan(0, 0, 0, 0, milliseconds);
            }
        }

        public long WarnThreshold
        {
            get { return long.Parse(ConfigurationManager.AppSettings["responsetime_threshold_inmillis"]); }
        }
    }
}