using System;
using System.Timers;
using PingIt.Lib.Processing;
using PingIt.Store.SQLite;

namespace PingIt.WindowsService
{
    internal class PingOwinServiceHost
    {
        private readonly Migrator _migrator;
        private IDisposable _webApp = null;
        private readonly PenguinProcessor _penguinProcessor;
        private readonly Timer _timer;
        private ConfigFileSettings _settings;

        public PingOwinServiceHost()
        {
            _settings = new ConfigFileSettings();
            _migrator = new Migrator(_settings);
            _penguinProcessor = new PenguinProcessor();
            _timer = new Timer(_settings.TickInterval);
            _timer.Elapsed += (sender, args) => _penguinProcessor.Tick();
        }

        public void Start()
        {
            _migrator.Migrate();
            //_webApp = WebApp.Start<SomeStuff>();
            _timer.Start();

        }

        public void Stop()
        {
            _webApp?.Dispose();
            _timer.Stop();
        }
    }
}