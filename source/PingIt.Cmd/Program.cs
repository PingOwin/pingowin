using System.Configuration;
using System.Linq;

namespace PingIt.Cmd
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
                var pinger = new Pinger();
                var pingResults = pinger.PingUrls(urls).GetAwaiter().GetResult();
                var output = transformer.Transform(pingResults);
                outputter.Output(output);
            }
        }

        public static IOutput Create()
        {
            if (ConfigurationManager.AppSettings["output"] == "1")
            {
                return new SlackOutputter(new SlackOutputConfig());
            }
            return new ConsoleOutputter();
        }
    }
}
