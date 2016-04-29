using System.Configuration;
using Nancy;
using Nancy.Owin;
using Owin;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder)
        {
            StaticConfiguration.DisableErrorTraces = false;
            var conf = new NancyOptions
            {
                Bootstrapper = new PingOwinWebBootstrapper()
            };
            
            appBuilder.UseNancy(conf);
            return appBuilder;
        }

        internal class ConfigFileSettingsDuplicate : IDatabaseSettings
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
}
