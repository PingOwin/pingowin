using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using PingOwin.Web;

namespace PingOwin.WindowsService
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UsePingOwinFrontend();
        }
    }
}
