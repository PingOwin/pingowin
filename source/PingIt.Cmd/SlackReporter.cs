using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace PingIt.Cmd
{
    public class SlackReporter
    {
        private readonly IOutput _outputter;

        public SlackReporter(IOutput outputter)
        {
            _outputter = outputter;
        }

        public void ReportToSlack(IEnumerable<PingResponse> responses)
        {
            var configuredLevel = (Level)Enum.Parse(typeof(Level), ConfigurationManager.AppSettings["level"]);
            var sb = new StringBuilder();
            foreach (var response in responses)
            {
                string text = string.Empty;
                switch (response.Level)
                {
                    case Level.Error:
                        text = $":x: {response.StatusCodeText} {response.Url} [{response.ResponseTime}ms] {response.ErrorMsg} <!channel>";
                        break;
                    case Level.Warn:
                        text = $":warning: {response.StatusCodeText} {response.Url}  [{response.ResponseTime}ms] {response.ErrorMsg}";
                        break;
                    case Level.OK:
                        text = $":white_check_mark: {response.StatusCodeText} {response.Url} [{response.ResponseTime}ms]";
                        break;
                }

                if (configuredLevel >= response.Level)
                {
                    sb.AppendLine($"{text}");
                }
            }


            var textToReport = sb.ToString();
            if (!string.IsNullOrEmpty(textToReport))
            {
                _outputter.Output(textToReport);
            }
        }

        public void ReportDebugToSlack(string[] urls)
        {
            var sb = new StringBuilder();
            sb.AppendLine("```");
            foreach (var url in urls)
            {
                sb.AppendLine($"{url}");
            }
            sb.AppendLine("```");
            _outputter.Output(sb.ToString());
        }


    }

    public interface IOutput
    {
        void Output(string text);
    }

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

    public class ConsoleOutputter : IOutput
    {
        public void Output(string text)
        {
            Console.WriteLine(text);
        }
    }
}