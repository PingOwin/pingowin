using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;

namespace PingIt.Cmd
{
    public class SlackReporter
    {
        public static void ReportToSlack(IEnumerable<PingResponse> responses)
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
                Report(textToReport);
            }
        }

        public static void ReportDebugToSlack(string[] urls)
        {
            var sb = new StringBuilder();
            sb.AppendLine("```");
            foreach (var url in urls)
            {
                sb.AppendLine($"{url}");
            }
            sb.AppendLine("```");
            Report(sb.ToString());
        }

        public static void Report(string text)
        {
            var slack = new HttpClient();
            var token = ConfigurationManager.AppSettings["token"];
            var channel = ConfigurationManager.AppSettings["channel"];
            var iconUrl = ConfigurationManager.AppSettings["icon_url"];
            var username = Environment.MachineName;

            var slackUri = $"https://slack.com/api/chat.postMessage?token={token}&channel={channel}&text={text}&username={username}&icon_url={iconUrl}";

            if (ConfigurationManager.AppSettings["output"] == "1")
            {
                slack.PostAsync(slackUri, null).GetAwaiter().GetResult();
            }
            else
            {
                Console.WriteLine(text);
            }
        }
    }
}