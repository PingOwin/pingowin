using System;
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

            app.UsePingOwinFrontend(new IisHackToLookupViewsBootstrapper());
        }
    }

    public class IisHackToLookupViewsBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add((viewName, model, context) => string.Concat("bin/views/", viewName));
        }
    }
}
