using Microsoft.AspNet.SignalR;
using Serenity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CaseManagement.Notification
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public string GetUserId(IRequest request)
        {
            var userId = string.Empty;
            if (Authorization.IsLoggedIn)
            {
                userId = Authorization.UserDefinition.Id;
            }
            return userId;
        }
    }
}