using System.Net.Http;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Notifiers.Slack
{
    public class SlackNotifier : INotify
    {
        private readonly ISlackOutputConfig _slackOutputConfig;

        public SlackNotifier(ISlackOutputConfig slackOutputConfig)
        {
            _slackOutputConfig = slackOutputConfig;
        }

        public Task Notify(string text)
        {
            var token = _slackOutputConfig.Token;
            var channel = _slackOutputConfig.Channel;
            var iconUrl = _slackOutputConfig.IconUrl;
            var username = _slackOutputConfig.Username;

            var slack = new HttpClient();
            var slackUri = $"https://slack.com/api/chat.postMessage?token={token}&channel={channel}&text={text}&username={username}&icon_url={iconUrl}";
            return slack.PostAsync(slackUri, null);
        }
    }
}