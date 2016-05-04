using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Hosting;
using Microsoft.Owin;
using Owin;
using PingIt.Lib;
using PingIt.Lib.Processing;
using PingOwin.Core.Store.SQLite;
using PingOwin;
using PingOwin.Web;

[assembly: OwinStartup(typeof(IisExpress.Startup))]

namespace IisExpress
{
    public class Startup
    {
        private Timer _timer;
        private PenguinProcessor _processor;

        public void Configuration(IAppBuilder app)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory);

            var databaseSettings = new DbSettings(connectionString);
            var migrator = new Migrator(databaseSettings);
            migrator.Migrate();

            _processor = CreateProcessor(databaseSettings);

            _timer = new Timer(5000);
            _timer.Elapsed += TickOnBackgroundThread;
            _timer.Start();

  

            app.UsePingOwinFrontend(connectionString);
        }

        private void TickOnBackgroundThread(object sender, ElapsedEventArgs e)
        {
            HostingEnvironment.QueueBackgroundWorkItem(cancellationToken =>
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    _timer.Stop();
                }
                else
                {
                    _processor.Tick().GetAwaiter().GetResult();
                }
            });
        }


        private PenguinProcessor CreateProcessor(DbSettings databaseSettings)
        {
            var pingConfiguration = new PingConfiguration();
            return new PenguinProcessor(new Pinger(pingConfiguration), new PenguinRepository(databaseSettings),
                new PenguinResultsRepository(databaseSettings));
        }
    }

    public class PingConfiguration : IPingConfiguration
    {
        public TimeSpan RequestTimeOut
        {
            get
            {
                var milliseconds = int.Parse(ConfigurationManager.AppSettings["timeoutInMs"]);
                return new TimeSpan(0, 0, 0, 0, milliseconds);
            }
        }

        public long WarnThreshold
        {
            get { return long.Parse(ConfigurationManager.AppSettings["responsetime_threshold_inmillis"]); }
        }
    }

    public class DbSettings : IDatabaseSettings
    {
        public DbSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
