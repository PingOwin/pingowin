using System;
using System.Configuration;
using System.Net.Http;

namespace PingIt.Cmd
{
    public class SlackOutputter : IOutput
    {
        public void Output(string text)
        {
            var slack = new HttpClient();
            var token = ConfigurationManager.AppSettings["token"];
            var channel = ConfigurationManager.AppSettings["channel"];
            var iconUrl = ConfigurationManager.AppSettings["icon_url"];
            var username = Environment.MachineName;
            var slackUri = $"https://slack.com/api/chat.postMessage?token={token}&channel={channel}&text={text}&username={username}&icon_url={iconUrl}";
            slack.PostAsync(slackUri, null).GetAwaiter().GetResult();
        }
    }
}