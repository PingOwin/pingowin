using System;
using Microsoft.Owin.Hosting;

namespace PingOwin.TestHosts.WindowsService
{
    internal class PingOwinServiceHost
    {
        private IDisposable _webApp;

        public void Start()
        {
            _webApp = WebApp.Start<Startup>("http://localhost:7788");
        }

        public void Stop()
        {
            _webApp?.Dispose();
        }
    }
}