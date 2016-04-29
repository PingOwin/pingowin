using System.Configuration;
using Nancy;
using Nancy.Owin;
using Nancy.TinyIoc;
using Owin;
using PingIt.Lib;
using PingIt.Store.SQLite;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder)
        {
            StaticConfiguration.DisableErrorTraces = false;
            NancyOptions conf = new NancyOptions
            {
                Bootstrapper = new iishackbootstrapper()
            };

            
            appBuilder.UseNancy(conf);
            return appBuilder;
        }

        public class iishackbootstrapper : DefaultNancyBootstrapper 
        {
            protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
            {
                Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("bin/views/", viewName));
            }

            protected override void ConfigureApplicationContainer(TinyIoCContainer container)
            {
                container.Register<IDatabaseSettings, ConfigFileSettingsDuplicate>();
                container.Register<IPenguinRepository, PenguinRepository>();
                base.ConfigureApplicationContainer(container);
            }
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
