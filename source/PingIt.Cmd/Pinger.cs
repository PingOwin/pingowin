using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace PingIt.Cmd
{
    public class Pinger
    {
        public static IEnumerable<PingResponse> PingUrls(string[] urls)
        {
            var pingTasks = (from url in urls select PingTaskCreator.Ping(url)).ToList();
            var pingResults = Task.WhenAll(pingTasks).GetAwaiter().GetResult().ToList();
            var orderedByLevel = pingResults.OrderBy(c => c.Level);
            return orderedByLevel;
        }
    }
}