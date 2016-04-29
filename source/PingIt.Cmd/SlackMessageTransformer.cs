using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace PingIt.Cmd
{
    public class SlackMessageTransformer
    {
        public string Transform(IEnumerable<PingResponse> responses)
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
            return sb.ToString();
        }

        public string ReportDebugToSlack(string[] urls)
        {
            var sb = new StringBuilder();
            sb.AppendLine("```");
            foreach (var url in urls)
            {
                sb.AppendLine($"{url}");
            }
            sb.AppendLine("```");
            return sb.ToString();
        }

    }
}