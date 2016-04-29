using Nancy;
using Nancy.Owin;
using Nancy.TinyIoc;
using Owin;

namespace PingOwin.Web
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder UsePingOwinFrontend(this IAppBuilder appBuilder, bool hack = false)
        {
            StaticConfiguration.DisableErrorTraces = false;
            NancyOptions conf = new NancyOptions
            {
                Bootstrapper = hack ? new iishackbootstrapper() : new DefaultNancyBootstrapper()
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
        }

    }
}
