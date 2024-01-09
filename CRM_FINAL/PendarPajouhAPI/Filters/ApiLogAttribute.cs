using Enterprise;
using PendarPajouhAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using CRM.WebAPI.Models.Shahkar.CustomClasses;
using CRM.Data;

namespace PendarPajouhAPI.Filters
{
    public class ApiLogAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            base.OnActionExecuting(actionContext);

            //در ابندا چک شود که آیا سرور سامانه شاهکار پاسخ گو هست یا خیر
            bool serverIsAccesible = HttpRequestHelpers.PingHost(ConfigurationHelpers.ShahkarWebApiIP);
            if (!serverIsAccesible)
            {
                Logger.WriteWarning("PendarPajouhAPI => Shahkar server is not accessible.");

                PendarWebApiResult shahkarResult = new PendarWebApiResult();
                shahkarResult.SystemError = ".در حال حاضر سرور سامانه شاهکار پاسخ نمیدهد ، چند دقیقه دیگر امتحان نمائید";
                shahkarResult.ShahkarServerIsAccessible = false;

                actionContext.Response = actionContext.Request.CreateResponse<PendarWebApiResult>(HttpStatusCode.ServiceUnavailable, shahkarResult);
            }
            else
            {
                Logger.WriteInfo("PendarPajouhAPI => {0} action is executing at {1}", actionContext.ActionDescriptor.ActionName, DB.GetServerDate().ToString("yyyy/MM/dd - hh:mm:ss"));
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnActionExecuted(actionExecutedContext);

            //زمانی پراپرتی مقدار دهی میشود که در اجرای اکشن خطا رخ داده باشد
            var keyValue = actionExecutedContext.Request.Properties.FirstOrDefault(ky => ky.Key.Equals("HasError"));
            bool actionExecutedWithError = Convert.ToBoolean(keyValue.Value);
            
            if (actionExecutedWithError)
            {
                Logger.WriteError("PendarPajouhAPI => {0} action is executed at {1} with Error", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DB.GetServerDate().ToString("yyyy/MM/dd - hh:mm:ss"));
            }
            else
            {
                Logger.WriteInfo("PendarPajouhAPI => {0} action is executed at {1}", actionExecutedContext.ActionContext.ActionDescriptor.ActionName, DB.GetServerDate().ToString("yyyy/MM/dd - hh:mm:ss"));
            }
        }
    }
}