using CRM.Data;
using Enterprise;
using PendarPajouhAPI.App_Start;
using PendarPajouhAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace PendarPajouhAPI
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);

            GlobalConfiguration.Configuration.Filters.Add(new BasicAuthenticationAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ApiLogAttribute());
            GlobalConfiguration.Configuration.Filters.Add(new ExceptionLogAttribute());

            Logger.WriteInfo("PendarPajouhAPI => starts at {0}", DB.GetServerDate().ToString("yyyy/MM/dd - hh:mm:ss"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var ex = Server.GetLastError();

            Logger.WriteError("PendarPajouhAPI => UnhandledException caught in {1}", System.Reflection.MethodBase.GetCurrentMethod().Name);
            Logger.WriteException("PendarPajouhAPI => {0}", ex.Message);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Logger.WriteInfo("PendarPajouhAPI => ends at {0}", DB.GetServerDate().ToString("yyyy/MM/dd - hh:mm:ss"));
        }
    }
}