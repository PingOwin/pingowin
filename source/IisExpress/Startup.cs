using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Nancy;
using Nancy.TinyIoc;
using Owin;
using PingOwin.Web;

[assembly: OwinStartup(typeof(IisExpress.Startup))]

namespace IisExpress
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UsePingOwinFrontend();
        }
    }
}
