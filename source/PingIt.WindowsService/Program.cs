using System;
using PingIt.Store.SQLite;
using Serilog;
using Serilog.Core;
using Topshelf;

namespace PingIt.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole(outputTemplate: "{Timestamp:G} [{Level}] {SourceContext} {Message}{NewLine:l}{Exception:l}")
                .MinimumLevel.Debug()
                .CreateLogger();

            HostFactory.Run(x =>
            {
                x.UseSerilog();
                x.Service<PingOwinServiceHost>(s =>
                {
                    s.ConstructUsing(name => new PingOwinServiceHost()); //kernel.get?
                    s.WhenStarted(p => p.Start());
                    s.WhenStopped(p => p.Stop());
                });
                x.RunAsLocalSystem();
                x.SetDescription("PingOwin pinging service");
                x.SetDisplayName("PingOwin");
                x.SetServiceName("PingOwin");
            });
        }
    }
}
