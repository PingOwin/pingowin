using Owin;
using PingOwin.Web;

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
