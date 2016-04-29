using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PingIt.Cmd
{
    public class Pinger
    {
        public async Task<IEnumerable<PingResponse>> PingUrls(string[] urls)
        {
            var pingTasks = (from url in urls select PingTaskCreator.Ping(url)).ToList();
            var pingResults = await Task.WhenAll(pingTasks);
            var orderedByLevel = pingResults.OrderBy(c => c.Level);
            return orderedByLevel;
        }
    }
}