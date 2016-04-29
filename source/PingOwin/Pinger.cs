using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PingIt.Lib
{
    public class Pinger
    {
        private readonly PingTaskCreator _pingtaskCreator;

        public Pinger(IPingConfiguration config)
        {
            _pingtaskCreator = new PingTaskCreator(config);
        }

        public async Task<IEnumerable<PingResponse>> PingUrls(IEnumerable<string> urls)
        {
            var pingTasks = (from url in urls select _pingtaskCreator.Ping(url)).ToList();
            var pingResults = await Task.WhenAll(pingTasks);
            var orderedByLevel = pingResults.OrderBy(c => c.Level);
            return orderedByLevel;
        }
    }
}