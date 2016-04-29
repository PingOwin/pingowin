using System;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Hosting.Self;

namespace PingIt.Cmd.WebHost
{
    class Program
    {


        static void Main(string[] args)
        {
            var config = new HostConfiguration {};
            config.UrlReservations.CreateAutomatically = true;
            INancyBootstrapper bootstrapper = new DefaultNancyBootstrapper();
            StaticConfiguration.DisableErrorTraces = false;

            using (var host = new NancyHost(bootstrapper, config, new Uri("http://localhost:1337")))
            {
                host.Start();
                Console.WriteLine("Running on http://localhost:1337");
                Console.ReadLine();
            }
        }
    }

    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/"] = x =>
            {
                return "hello world!";
            };
        }
    }
}
