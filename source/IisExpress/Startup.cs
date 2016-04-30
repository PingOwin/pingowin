using System;
using System.Configuration;
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
            var connectionString = ConfigurationManager.ConnectionStrings["Database"].ConnectionString.Replace("{AppDir}", AppDomain.CurrentDomain.BaseDirectory);

            var migrator = new Migrator(new DbSettings(connectionString));
            migrator.Migrate();
            app.UsePingOwinFrontend(connectionString);
        }
    }

    public class DbSettings : IDatabaseSettings
    {
        public DbSettings(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }
    }
}
