using Nancy;
using Owin;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder)
        {
            StaticConfiguration.DisableErrorTraces = false;
            appBuilder.UseNancy();
            return appBuilder;
        }

    }
}
