using System.Collections.Generic;
using System.Text;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Processing;

namespace PingOwin.Core.Notifiers.Slack
{
    public class SlackMessageTransformer : ITransformResponses
    {
        private readonly Level _level;

        public SlackMessageTransformer(Level level)
        {
            _level = level;
        }

        public string Transform(IEnumerable<PingResponse> responses)
        {
            
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

                if (_level >= response.Level)
                {
                    sb.AppendLine($"{text}");
                }
            }
            return sb.ToString();
        }
    }
}