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
        private PenguinProcessor _penguinProcessor;
        private Timer _timer;

        public PingOwinServiceHost()
        {
            _migrator = new Migrator(new ConfigFileSettings());
            _penguinProcessor = new PenguinProcessor();
            _timer = new Timer(5000);
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