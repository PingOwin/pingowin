using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.TinyIoc;
using PingOwin.Core.Interfaces;
using PingOwin.Core.Store.SQLite;

namespace PingOwin.Core.Frontend
{
    public class PingOwinWebBootstrapper : DefaultNancyBootstrapper 
    {
        private readonly IDatabaseSettings _databaseSettings;

        public PingOwinWebBootstrapper(IDatabaseSettings databaseSettings)
        {
            _databaseSettings = databaseSettings;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            Conventions.ViewLocationConventions.Add((viewName, model, context) => String.Concat("bin/views/", viewName));
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IDatabaseSettings>((c,p) => _databaseSettings);
            container.Register<IPenguinRepository, PenguinRepository>();
            container.Register<IPenguinResultsRepository, PenguinResultsRepository>();
            base.ConfigureApplicationContainer(container);
        }

        protected override IEnumerable<Func<Assembly, bool>> AutoRegisterIgnoredAssemblies
        {
            get
            {
                var defaults = new List<Func<Assembly, bool>>();
                defaults.AddRange(DefaultAutoRegisterIgnoredAssemblies);
                defaults.Add(asm => asm.FullName.StartsWith("Ping", StringComparison.Ordinal));
                return defaults;
            }
        }
    }
}