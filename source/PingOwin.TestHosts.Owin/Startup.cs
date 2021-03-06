﻿using System;
using System.Web.Hosting;
using Owin;
using PingOwin.Core.Frontend;

namespace PingOwin.TestHosts.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var pingOwinOptions = new PingOwinOptions
            {
                PathToDb = AppDomain.CurrentDomain.BaseDirectory,
                StartService = HostingEnvironment.QueueBackgroundWorkItem
            };
            app.UsePingOwin(pingOwinOptions);
        }
    }
}
