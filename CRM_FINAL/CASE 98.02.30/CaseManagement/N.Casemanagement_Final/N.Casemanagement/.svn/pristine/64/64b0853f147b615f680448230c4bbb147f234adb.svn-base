using Serenity;
using System;
using System.Collections.Generic;
using System.Data;
using Serenity.Services;
using System.Linq;
using System.Web;
using Serenity.Data;

namespace CaseManagement.Case
{
    public static class ActivityRequestLogDB
    {
        public static void SaveActivityRequestLog(long activityRequestID, int statusID, RequestAction actionID, IDbConnection connection)
        {
            Entities.ActivityRequestLogRow log = new Entities.ActivityRequestLogRow();

            log.ActivityRequestId = activityRequestID;
            log.StatusId = statusID;
            log.ActionID = actionID;
            log.UserId = int.Parse(Authorization.UserId);
            log.InsertDate = DateTime.Now;

            connection.Insert<Entities.ActivityRequestLogRow>(log);
        }
    }
}