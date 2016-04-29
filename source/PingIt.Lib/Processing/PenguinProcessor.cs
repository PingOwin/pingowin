using System;
using Serilog;

namespace PingIt.Lib.Processing
{
    public class PenguinProcessor
    {
        private readonly ILogger _log = Log.ForContext<PenguinProcessor>();

        public void Tick()
        {
            _log.Information("Here I Am");
        }
    }
}
