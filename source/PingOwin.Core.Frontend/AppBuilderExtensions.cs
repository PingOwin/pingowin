using System;
using System.Threading;
using Nancy;
using Nancy.Owin;
using Owin;
using PingOwin.Core.Store.SQLite;
using Timer = System.Timers.Timer;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        private static Timer _timer;

        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder, PingOwinOptions options = null)
        {
            options = options ?? new PingOwinOptions();

            var connectionString = $"Data Source={options.PathToDb}pingowin.db;Version=3;Pooling=True;Max Pool Size=100;";

            var databaseSettings = new DbSettings(connectionString);
            var migrator = new Migrator(databaseSettings);
            migrator.Migrate();
            
            var conf = new NancyOptions
            {
                Bootstrapper = new PingOwinWebBootstrapper(connectionString)
            };
            
            appBuilder.UseNancy(conf);

            var factory = new ServiceRunner(options, databaseSettings);
            factory.Start();
       
            return appBuilder;
        }
    }


    public class PingOwinOptions
    {
        public PingOwinOptions()
        {
            PathToDb = AppDomain.CurrentDomain.BaseDirectory;
            PingIntervalInMillis = 5000;
        }

        public string PathToDb { get; set; }
        public Action<Action<CancellationToken>> StartService { get; set; }
        public double PingIntervalInMillis { get; set; }
    }
}
