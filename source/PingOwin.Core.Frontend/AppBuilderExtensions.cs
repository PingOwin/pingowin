using Nancy;
using Nancy.Owin;
using Owin;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder, string connectionString)
        {
            StaticConfiguration.DisableErrorTraces = false;
            var conf = new NancyOptions
            {
                Bootstrapper = new PingOwinWebBootstrapper(connectionString)
            };
            
            appBuilder.UseNancy(conf);
            return appBuilder;
        }
    }
}
