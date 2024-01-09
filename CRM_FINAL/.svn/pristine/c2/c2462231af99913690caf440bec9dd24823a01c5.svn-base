using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using CRM.WebAPI.Models.Shahkar.CustomClasses;

namespace PendarPajouhAPI.Filters
{
    public class ExceptionLogAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            Logger.WriteError("PendarPajouhAPI => Following exception catght in {0}", MethodBase.GetCurrentMethod().Name);
            Logger.WriteException("PendarPajouhAPI => {0}", actionExecutedContext.Exception.Message);
            Logger.WriteException("PendarPajouhAPI => {0}", actionExecutedContext.Exception.StackTrace);

            PendarWebApiResult shahkarResult = new PendarWebApiResult();
            shahkarResult.SystemError = ".خطای سیستمی رخ داده است";

            actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse<PendarWebApiResult>(HttpStatusCode.Gone, shahkarResult);
        }
    }
}