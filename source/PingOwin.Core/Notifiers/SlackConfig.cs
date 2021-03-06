using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Notifiers
{
    public class SlackConfig : ISlackOutputConfig
    {
        public string Token { get; set; }
        public string Channel { get; set; }
        public string IconUrl { get; set; }
        public string Username { get; set; }
    }
}