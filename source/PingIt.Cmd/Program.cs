using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PingIt.Cmd;

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
                SlackReporter.ReportDebugToSlack(urls);
            }
            else
            {
                var pingResults = Pinger.PingUrls(urls);
                SlackReporter.ReportToSlack(pingResults);
            }
        }
    }
}
