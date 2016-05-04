using System;
using Nancy.Owin;
using Owin;
using PingOwin.Core.Processing;
using PingOwin.Core.Store.SQLite;
using Timer = System.Timers.Timer;

namespace PingOwin.Core.Frontend
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

            var pingConfiguration = new PingConfiguration
            {
                RequestTimeOut = new TimeSpan(0,0,0,0,options.RequestTimeOut),
                WarnThreshold = options.WarnThreshold
            };
            var processor = IoCFactory.CreateProcessor(pingConfiguration, databaseSettings, NotifierType.Konsole, Level.OK, new SlackConfig());
            var serviceRunner = new ServiceRunner(options, processor);
            serviceRunner.StartBackgroundThread();
       
            return appBuilder;
        }
    }
}
