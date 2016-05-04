using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PingOwin.Core.Interfaces;

namespace PingOwin.Core.Processing
{
    public class Pinger
    {
        private readonly HttpClientPinger _httpClientPinger;

        public Pinger(IPingConfiguration config)
        {
            _httpClientPinger = new HttpClientPinger(config);
        }
    }
}