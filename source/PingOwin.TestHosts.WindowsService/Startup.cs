using Owin;
using PingOwin.Core.Frontend;

namespace PingOwin.TestHosts.WindowsService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UsePingOwinFrontend();
        }
    }
}
