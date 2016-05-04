using System.Timers;
using PingOwin.Core.Processing;
using PingOwin.Core.Store.SQLite;

namespace PingOwin.Core.Frontend
{
    public class ServiceRunner
    {
        private readonly PingOwinOptions _options;
        private readonly Timer _timer;
        private readonly PenguinProcessor _processor;

        public ServiceRunner(PingOwinOptions options, DbSettings dbSettings)
        {
            _options = options;
            _timer = new Timer(options.PingIntervalInMillis);
            _timer.Elapsed += TickOnBackgroundThread;
            _timer.Start();
            _processor = CreateProcessor(dbSettings);
        }

        public void Start()
        {
            _timer.Start();
        }

        private void TickOnBackgroundThread(object sender, ElapsedEventArgs e)
        {
            _options.StartService(cancellationToken =>
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

        private static PenguinProcessor CreateProcessor(DbSettings databaseSettings)
        {
            var pingConfiguration = new PingConfiguration();
            return new PenguinProcessor(new Pinger(pingConfiguration), new PenguinRepository(databaseSettings),
                new PenguinResultsRepository(databaseSettings));
        }
    }
}