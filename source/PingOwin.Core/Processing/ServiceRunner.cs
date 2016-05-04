using System.Timers;
using Timer = System.Timers.Timer;

namespace PingOwin.Core.Processing
{
    public class ServiceRunner
    {
        private readonly ServiceRunnerOptions _options;
        private readonly Timer _timer;
        private readonly PenguinProcessor _processor;

        public ServiceRunner(ServiceRunnerOptions options, PenguinProcessor processor)
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