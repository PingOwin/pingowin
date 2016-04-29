using System;
using System.Configuration;

namespace PingIt.Cmd
{
    public class SlackOutputConfig : ISlackOutputConfig
    {
        public string Token { get { return ConfigurationManager.AppSettings["token"]; } }
        public string Channel { get { return ConfigurationManager.AppSettings["channel"];  } }
        public string IconUrl { get { return ConfigurationManager.AppSettings["icon_url"];  } }
        public string Username { get { return Environment.MachineName;  } }
    }
}