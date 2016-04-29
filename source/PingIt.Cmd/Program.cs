﻿using System.Configuration;
using System.Linq;
using PingIt.Lib;
using PingIt.Store.SQLite;

namespace PingIt.Cmd
{
    class Program
    {

        static void Main(string[] args)
        {
            var pingConfiguration = new PingConfiguration();
            var repo = new PenguinRepository(pingConfiguration);
            new Migrator(pingConfiguration).Migrate();
            var urls = repo.GetAll().GetAwaiter().GetResult().Select(c => c.Url);
            var transformer = new SlackMessageTransformer(Level.OK);

            var outputter = CreateOutputter();

            if (args.Length >= 1 && args.First() == "debug")
            {
                var debugInfo = transformer.TransformDebugInfo(urls);
                outputter.SendToOutput(debugInfo);
            }
            else
            {
               
                var pinger = new Pinger(pingConfiguration);
                var pingResults = pinger.PingUrls(urls).GetAwaiter().GetResult();
                var output = transformer.Transform(pingResults);
                outputter.SendToOutput(output).GetAwaiter().GetResult();
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
            return new SlackMessageTransformer(Level.OK);
        }
    }
}
