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
            var transformer = new SlackMessageTransformer();
            var outputter = Create();

            if (args.Length >= 1 && args.First() == "debug")
            {
                var debugInfo = transformer.TransformDebugInfo(urls);
                outputter.Output(debugInfo);
            }
            else
            {
                var pingResults = Pinger.PingUrls(urls);
                var output = transformer.Transform(pingResults);
                outputter.Output(output);
            }
        }

        public static IOutput Create()
        {
            if (ConfigurationManager.AppSettings["output"] == "1")
            {
                return new SlackOutputter();
            }
            return new ConsoleOutputter();
        }
    }
}
