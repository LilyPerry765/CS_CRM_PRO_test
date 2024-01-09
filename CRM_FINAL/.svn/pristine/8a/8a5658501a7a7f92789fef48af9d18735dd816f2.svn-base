using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace PendarPajouhAPI.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute
                (
                    name: "DefaultApi",
                    routeTemplate: "api/PendarPajouhCRM/{action}",
                    defaults: new { controller = "PendarPajouhCRM" }
                );

            //  خروجی فقط  ، جی سان میباشد
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            var jqueryFormatter = config.Formatters.FirstOrDefault(x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));
            config.Formatters.Remove(jqueryFormatter);
        }
    }
}