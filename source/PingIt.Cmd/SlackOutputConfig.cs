using System;
using System.Configuration;
using PingIt.Lib;

namespace PingIt.Cmd
{
    public class SlackOutputConfig : ISlackOutputConfig
    {
        public string Token { get { return ConfigurationManager.AppSettings["token"]; } }
        public string Channel { get { return ConfigurationManager.AppSettings["channel"];  } }
        public string IconUrl { get { return ConfigurationManager.AppSettings["icon_url"];  } }
        public string Username { get { return Environment.MachineName;  } }
    }

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