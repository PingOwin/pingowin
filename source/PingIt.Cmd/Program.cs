using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExternalPinger
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlsCsv = ConfigurationManager.AppSettings["urls"];
            var urls = urlsCsv.Split(';');

            if (args.Length >= 1 && args.First() == "debug")
            {
                ReportDebugToSlack(urls);
            }
            else
            {
                var pingTasks = (from url in urls select PingTask(url)).ToList();
                var pingResults = Task.WhenAll(pingTasks).GetAwaiter().GetResult().ToList();
                var orderedByLevel = pingResults.OrderBy(c => c.Level);
                ReportToSlack(orderedByLevel);
            }
        }

        private static async Task<PingResponse> PingTask(string url)
        {
            var canParse = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (!canParse || string.IsNullOrEmpty(url))
            {
                return new PingResponse { Url = url, ErrorMsg = "Could not parse/use this url from config" };
            }

            var httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 0, 0, int.Parse(ConfigurationManager.AppSettings["timeoutInMs"]));
            var stopWatch = Stopwatch.StartNew();
            try
            {

                var httpResponse = await httpClient.GetAsync(url);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, Level = Level.Error, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
                }

                if (httpResponse.IsSuccessStatusCode && stopWatch.ElapsedMilliseconds > (long.Parse(ConfigurationManager.AppSettings["responsetime_threshold_inmillis"])))
                {
                    var text = $"Responsetime > {ConfigurationManager.AppSettings["responsetime_threshold_inmillis"]}ms";
                    return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, ErrorMsg = text, Level = Level.Warn, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
                }

                return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, Level = Level.OK, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
            }
            catch (TaskCanceledException e)
            {
                var timeoutinms = ConfigurationManager.AppSettings["timeoutInMs"];
                return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, ErrorMsg = $"Request took longer than timeout of {timeoutinms}ms. Cancelled.", Level = Level.Error };
            }
            catch (HttpRequestException e)
            {
                return PingErrorException(url, e.ToString(), stopWatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                return PingErrorException(url, $"Unhandled exception occured: {e}", stopWatch.ElapsedMilliseconds);
            }
        }

        private static PingResponse PingErrorException(string url, string e, long elapsed)
        {
            var error = e.Length > 300 ? e.Substring(0, 300) + " ... " : e;
            return new PingResponse { Url = url, ResponseTime = elapsed, ErrorMsg = error, Level = Level.Error };
        }

        private static void ReportToSlack(IEnumerable<PingResponse> responses)
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

        private static void ReportDebugToSlack(string[] urls)
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

        private static void Report(string text)
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

        internal class PingResponse
        {
            public string Url { get; set; }
            public string ErrorMsg { get; set; }
            public long ResponseTime { get; set; }

            public Level Level { get; set; }
            public string StatusCodeText { get; set; }

            public bool HasErrors()
            {
                return Level == Level.Warn || Level == Level.Error;
            }
        }

        internal enum Level
        {
            Unknown = 0,
            Error = 1,
            Warn = 2,
            OK = 3
        }
    }
}
