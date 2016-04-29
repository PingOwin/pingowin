using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PingIt.Lib
{
    public class PingTaskCreator
    {
        public static async Task<PingResponse> Ping(string url)
        {
            var canParse = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (!canParse || string.IsNullOrEmpty(url))
            {
                return new PingResponse { Url = url, ErrorMsg = "Could not parse/use this url from config" };
            }

            var httpClient = new HttpClient
            {
                Timeout = new TimeSpan(0, 0, 0, 0, int.Parse(ConfigurationManager.AppSettings["timeoutInMs"]))
            };
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
                return ExceptionResponse(url, e.ToString(), stopWatch.ElapsedMilliseconds);
            }
            catch (Exception e)
            {
                return ExceptionResponse(url, $"Unhandled exception occured: {e}", stopWatch.ElapsedMilliseconds);
            }
        }

        private static PingResponse ExceptionResponse(string url, string e, long elapsed)
        {
            var error = e.Length > 300 ? e.Substring(0, 300) + " ... " : e;
            return new PingResponse { Url = url, ResponseTime = elapsed, ErrorMsg = error, Level = Level.Error };
        }
    }
}