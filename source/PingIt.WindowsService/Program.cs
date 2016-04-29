using System;
using PingIt.Store.SQLite;
using Topshelf;

namespace PingIt.WindowsService
{
    class Program
    {
        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
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
