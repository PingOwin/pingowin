using System;
using Nancy.Owin;
using Owin;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Notifiers;
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
                Bootstrapper = new PingOwinWebBootstrapper(databaseSettings)
            };
            
            appBuilder.UseNancy(conf);

            var pingConfiguration = new PingConfiguration
            {
                RequestTimeOut = new TimeSpan(0,0,0,0,options.RequestTimeOut),
                WarnThreshold = options.WarnThreshold
            };

            var notifierType = NotifierType.Konsole;
            var slackConfig = new SlackConfig();

            var notifierFactory = new NotifierFactory(notifierType, slackConfig);
            var transformerFactory = new TransformerFactory(notifierType, Level.OK);

            var processor = IoCFactory.CreateProcessor(pingConfiguration, databaseSettings, notifierFactory, transformerFactory);


            var serviceRunner = new ServiceRunner(new ServiceRunnerOptions
            {
                PingIntervalInMillis = options.PingIntervalInMillis,
                RunBackgroundThread = options.StartService
            }, processor);
            serviceRunner.StartBackgroundThread();
       
            return appBuilder;
        }
    }
}
