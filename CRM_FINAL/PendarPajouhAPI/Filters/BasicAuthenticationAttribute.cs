using Enterprise;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Net;
using System.Text;
using PendarPajouhAPI.Helpers;
using System.Reflection;

namespace PendarPajouhAPI.Filters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            try
            {
                if (actionContext.Request.Headers.Authorization == null || !actionContext.Request.Headers.Authorization.Scheme.Equals("Basic"))
                {
                    Logger.WriteError("PendarPajouhAPI => Basic Authentication is empty!");
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                }
                else
                {
                    string encodedIntoBase64Credentials = actionContext.Request.Headers.Authorization.Parameter;
                    byte[] encodedIntoBase64CredentialsBytes = Convert.FromBase64String(encodedIntoBase64Credentials);
                    string[] decodedCredentials = Encoding.UTF8.GetString(encodedIntoBase64CredentialsBytes).Split(':');
                    string userName = decodedCredentials[0];
                    string password = decodedCredentials[1];

                    if (SecurityHelpers.IsValidUser(userName, password))
                    {
                        Logger.WriteInfo("PendarPajouhAPI => {0}:{1} is authenticated successfully.", userName, password);
                    }
                    else
                    {
                        Logger.WriteWarning("PendarPajouhAPI => {0}:{1} is not authenticated.", userName, password);
                        actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Invalid");
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("PendarPajouhAPI => Following exception catght in {0}", MethodBase.GetCurrentMethod().Name);
                Logger.WriteException("PendarPajouhAPI => {0}", ex.Message);
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "InternalError");
            }
        }
    }
}