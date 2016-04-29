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

            var outputter = CreateOutputter();

            if (args.Length >= 1 && args.First() == "debug")
            {
                var debugInfo = transformer.TransformDebugInfo(urls);
                outputter.SendToOutput(debugInfo);
            }
            else
            {
                var pinger = new Pinger();
                var pingResults = pinger.PingUrls(urls).GetAwaiter().GetResult();
                var output = transformer.Transform(pingResults);
                outputter.SendToOutput(output);
            }
        }

        public static IOutput CreateOutputter()
        {
            if (ConfigurationManager.AppSettings["output"] == "1")
            {
                return new SlackOutputter(new SlackOutputConfig());
            }
            return new ConsoleOutputter();
        }

        public static ITransformResponses CreateTransformer()
        {
            return new SlackMessageTransformer();
        }
    }
}
