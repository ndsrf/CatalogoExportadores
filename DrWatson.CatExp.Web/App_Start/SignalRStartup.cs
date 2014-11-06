﻿using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DrWatson.CatExp.WebApi.App_Start.SignalRStartup))]

namespace DrWatson.CatExp.WebApi.App_Start
{
    public class SignalRStartup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.MapSignalR();
        }
    }
}
