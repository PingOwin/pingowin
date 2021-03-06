﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using Nancy.ViewEngines;
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
            //Conventions.ViewLocationConventions.Add((viewName, model, context) => String.Concat("bin/views/", viewName));
          
        }

        protected override NancyInternalConfiguration InternalConfiguration => NancyInternalConfiguration.WithOverrides(OnConfigurationBuilder);

        void OnConfigurationBuilder(NancyInternalConfiguration x)
        {
            x.ViewLocationProvider = typeof(ResourceViewLocationProvider);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            container.Register<IDatabaseSettings>((c,p) => _databaseSettings);
            container.Register<IPenguinRepository, PenguinRepository>();
            container.Register<IPenguinResultsRepository, PenguinResultsRepository>();
            var assembly = GetType().Assembly;
            ResourceViewLocationProvider.RootNamespaces.Add(assembly, "PingOwin.Core.Frontend.Views");
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