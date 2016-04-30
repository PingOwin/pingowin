using System;
using System.Timers;
using Microsoft.Owin.Hosting;
using PingIt.Lib;
using PingIt.Lib.Processing;
using PingIt.Store.SQLite;
using PingOwin.WindowsService;

namespace PingIt.WindowsService
{
    internal class PingOwinServiceHost
    {
        private readonly Migrator _migrator;
        private IDisposable _webApp = null;
        private PenguinProcessor _penguinProcessor;
        private readonly Timer _timer;
        private ConfigFileSettings _settings;

        public PingOwinServiceHost()
        {
            _settings = new ConfigFileSettings();
            _migrator = new Migrator(_settings);
            _penguinProcessor = CreateProcessor();
            _timer = new Timer(_settings.TickInterval);
            _timer.Elapsed += (sender, args) => _penguinProcessor.Tick();
        }

        private PenguinProcessor CreateProcessor()
        {
            var pingConfiguration = new PingConfiguration();
            return new PenguinProcessor(new Pinger(pingConfiguration), new PenguinRepository(pingConfiguration),
                new PenguinResultsRepository(pingConfiguration));
        }

        public void Start()
        {
            _migrator.Migrate();
            _webApp = WebApp.Start<Startup>("http://localhost:7788");
            _timer.Start();

        }

        public void Stop()
        {
            _webApp?.Dispose();
            _timer.Stop();
        }
    }
}