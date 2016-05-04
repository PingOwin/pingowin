using System;
using System.Web.Hosting;
using Microsoft.Owin;
using Owin;
using PingOwin.Web;

[assembly: OwinStartup(typeof(IisExpress.Startup))]

namespace IisExpress
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var pingOwinOptions = new PingOwinOptions
            {
                PathToDb = AppDomain.CurrentDomain.BaseDirectory,
                StartService = HostingEnvironment.QueueBackgroundWorkItem
            };

            app.UsePingOwinFrontend(pingOwinOptions);
        }

 
    }
}
