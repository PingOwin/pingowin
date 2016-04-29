using Nancy;
using Nancy.Owin;
using Owin;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder, DefaultNancyBootstrapper bootstrapper = null)
        {
            StaticConfiguration.DisableErrorTraces = false;
            NancyOptions conf = new NancyOptions
            {
                Bootstrapper = bootstrapper ?? new DefaultNancyBootstrapper()
            };
            appBuilder.UseNancy(conf);
            return appBuilder;
        }

    }
}
