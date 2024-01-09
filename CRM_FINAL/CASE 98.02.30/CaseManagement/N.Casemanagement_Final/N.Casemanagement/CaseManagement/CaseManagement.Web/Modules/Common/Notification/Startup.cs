using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(CaseManagement.Notification.Startup))]

namespace CaseManagement.Notification
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var idProvider = new CustomUserIdProvider();

            // to send notifications to single users by UserId instead of by Username
            //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}