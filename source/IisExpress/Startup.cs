using System;
using System.Configuration;
using System.Data.SQLite;
using System.Threading.Tasks;
using Microsoft.Owin;
using Nancy;
using Nancy.TinyIoc;
using Owin;
using PingIt.Store.SQLite;
using PingOwin;
using PingOwin.Web;

[assembly: OwinStartup(typeof(IisExpress.Startup))]

namespace IisExpress
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var settings = new ConfigFileSettings();
            var migrator = new Migrator(settings);
            migrator.Migrate();
            app.UsePingOwinFrontend();
        }
    }

    internal class ConfigFileSettings : IDatabaseSettings
    {
        public string ConnectionString => ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
        public int TickInterval => GetAppsettingInt("TickInterval", 5000);

        private int GetAppsettingInt(string key, int defaultValue)
        {
            int dummy;
            return int.TryParse(ConfigurationManager.AppSettings[key], out dummy) ? dummy : defaultValue;
        }
    }


}
