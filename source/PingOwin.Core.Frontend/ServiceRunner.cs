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

        public ServiceRunner(PingOwinOptions options, PenguinProcessor processor)
        {
            _options = options;
            _timer = new Timer(options.PingIntervalInMillis);
            _timer.Elapsed += TickOnBackgroundThread;
            _timer.Start();
            _processor = processor;
        }

        public void StartBackgroundThread()
        {
            _timer.Start();
        }

        private void TickOnBackgroundThread(object sender, ElapsedEventArgs e)
        {
            if (_options.StartService != null)
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
        }
    }
}