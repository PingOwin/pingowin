using System.Configuration;
using System.Linq;
using PingIt.Cmd;

namespace ExternalPinger
{
    class Program
    {
        static void Main(string[] args)
        {
            var urlsCsv = ConfigurationManager.AppSettings["urls"];
            var urls = urlsCsv.Split(';');
            var reporter = Create();
            if (args.Length >= 1 && args.First() == "debug")
            {
                reporter.ReportDebugToSlack(urls);
            }
            else
            {
                var pingResults = Pinger.PingUrls(urls);
                reporter.ReportToSlack(pingResults);
            }
        }

        public static SlackReporter Create()
        {
            if (ConfigurationManager.AppSettings["output"] == "1")
            {
                return new SlackReporter(new SlackOutputter());
            }
            return new SlackReporter(new ConsoleOutputter());
        }
    }
}
