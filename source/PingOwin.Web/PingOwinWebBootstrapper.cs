using System;
using Nancy;
using Nancy.TinyIoc;
using PingIt.Lib;
using PingIt.Store.SQLite;

namespace PingOwin.Web
{
    public class PingOwinWebBootstrapper : DefaultNancyBootstrapper 
    {
        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add((viewName, model, context) => String.Concat("bin/views/", viewName));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IDatabaseSettings, AppBuilderExtensions.ConfigFileSettingsDuplicate>();
            container.Register<IPenguinRepository, PenguinRepository>();
            container.Register<IPenguinResultsRepository, PenguinResultsRepository>();
            base.ConfigureApplicationContainer(container);
        }
    }
}