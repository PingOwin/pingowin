using System;

using System.Data.SQLite;
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
            var cmd = new SQLiteCommand();
            cmd.CommandText = "This is adummy";
            app.UsePingOwinFrontend();
        }
    }
}
