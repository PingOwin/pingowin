using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Processing
{
    public class HttpClientPinger
    {
        private readonly IPingConfiguration _pingConfiguration;

        public HttpClientPinger(IPingConfiguration pingConfiguration)
        {
            _pingConfiguration = pingConfiguration;
        }

        public async Task<PingResponse> Ping(string url)
        {
            var canParse = Uri.IsWellFormedUriString(url, UriKind.Absolute);
            if (!canParse || string.IsNullOrEmpty(url))
            {
                return new PingResponse { Url = url, ErrorMsg = "Could not parse/use this url from config" };
            }

            var httpClient = new HttpClient
            {
                Timeout = _pingConfiguration.RequestTimeOut
            };

            var stopWatch = Stopwatch.StartNew();
            try
            {

                var httpResponse = await httpClient.GetAsync(url);

                if (!httpResponse.IsSuccessStatusCode)
                {
                    return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, Level = Level.Error, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
                }

                if (httpResponse.IsSuccessStatusCode && stopWatch.ElapsedMilliseconds > _pingConfiguration.WarnThreshold)
                {
                    var text = $"Responsetime > {_pingConfiguration.WarnThreshold}ms";
                    return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, ErrorMsg = text, Level = Level.Warn, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
                }

                return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, Level = Level.OK, StatusCodeText = $"{(int)httpResponse.StatusCode} {httpResponse.StatusCode}" };
            }
            catch (TaskCanceledException)
            {
                return new PingResponse { Url = url, ResponseTime = stopWatch.ElapsedMilliseconds, ErrorMsg = $"Request took longer than timeout of {_pingConfiguration.RequestTimeOut}ms. Cancelled.", Level = Level.Error };
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