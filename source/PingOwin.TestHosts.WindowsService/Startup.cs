using System.Threading.Tasks;
using Owin;
using PingOwin.Core.Frontend;

namespace PingOwin.TestHosts.WindowsService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // TODO : find a common way to do background threads for web hosts and windows services
            app.UsePingOwinFrontend(new PingOwinOptions
            {
                PathToDb = "C:\\temp\\"
            });
        }
    }
}
