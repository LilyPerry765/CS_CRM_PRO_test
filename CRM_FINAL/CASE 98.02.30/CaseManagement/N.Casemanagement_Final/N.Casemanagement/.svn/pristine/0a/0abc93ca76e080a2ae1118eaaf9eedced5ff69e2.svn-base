
namespace CaseManagement.Common.Pages
{
    using Case;
    using Case.Entities;
    using Serenity;
    using Serenity.Data;
    using Serenity.Services;
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using System.Collections.Generic;
    using Serenity.Extensibility;
    using System.Data;
    using System.Linq;
    using System.Reflection;
    using Serenity.Web;
    using CaseManagement.Administration.Entities;
    using System.Net;
    using System.Net.Sockets;

    [RoutePrefix("Dashboard"), Route("{action=index}")]
    public class DashboardController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.General), HttpGet, Route("~/")]
        public ActionResult Index()
        {
            var cachedModel = TwoLevelCache.GetLocalStoreOnly("DashboardPageModel", TimeSpan.FromSeconds(2),
                ActivityRequestRow.Fields.GenerationKey, () =>
                {
                    int userID1 = int.Parse(Serenity.Authorization.UserId);
                    int? provinceID = 0;
                    List<int> roleIDs = null;
                    List<int> stepIDs = null;
                    List<int> statusIDs = null;

                    var model = new DashboardPageModel();
                    var activity = ActivityRequestRow.Fields;
                    using (var connection = SqlConnections.NewFor<ActivityRequestRow>())
                    {
                        provinceID = connection.List<UserRow>().Where(t => t.UserId == userID1).Select(t => (int?)t.ProvinceId).SingleOrDefault();
                        roleIDs = connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID1).Select(t => (int)t.RoleId).ToList();

                        if (roleIDs != null && roleIDs.Count != 0)
                        {
                            stepIDs = connection.List<Administration.Entities.RoleStepRow>().Where(t => roleIDs.Contains((int)t.RoleId)).Select(s => (int)s.StepId).ToList();
                            statusIDs = connection.List<WorkFlow.Entities.WorkFlowStatusRow>().Where(t => stepIDs.Contains((int)t.StepId)).Select(s => (int)s.Id).ToList();
                        }

                        DateTime startDate = new DateTime(2018, 3, 21);

                        if (provinceID != null)
                        {
                            model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();
                            model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();
                            model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.EndDate != null && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false).Count();
                            model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false && t.EndDate == null).Count();
                        }
                        else
                        {
                            if (statusIDs != null)
                            {
                                model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();
                                model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();
                                model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.EndDate != null && t.IsDeleted == false).Count();
                                model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.IsDeleted == false && t.EndDate == null).Count();
                            }
                        }
                    }

                    return model;
                });


            int userID = int.Parse(Serenity.Authorization.UserId);



            using (var connection = SqlConnections.NewFor<Administration.Entities.LogRow>())
            {


                //DateTime now = new DBHelper().GetServerDate(); //DateTime.Now;
               // List<LogRow> logList = connection.List<LogRow>().Where(t => t.UserId == userID && t.InsertDate.Value.Year == now.Year && t.InsertDate.Value.Month == now.Month && t.InsertDate.Value.Day == now.Day && t.ActionID == Administration.ActionLog.Login).ToList();
               // int logCount = connection.List<LogRow>().Where(t => t.UserId == userID && t.InsertDate.Value.Year == now.Year && t.InsertDate.Value.Month == now.Month && t.InsertDate.Value.Day == now.Day && t.ActionID == Administration.ActionLog.Login).Count();
                int logCount = 0;
                DBHelper dbh = new DBHelper();
                object Scalar =   dbh.ExecuteScalar(string.Format("SELECT COUNT(*) FROM [LOG] WHERE UserID={0} AND InsertDate>= CAST (GetDate() AS Date) AND ActionID={1} " , userID,(int)Administration.ActionLog.Login));
                logCount = (int)Scalar;
                if (logCount == null || logCount == 0)
                    Helper.SaveLog("ورود", "کاربر", userID, Serenity.Authorization.UserDefinition.DisplayName, "", connection, Administration.ActionLog.Login);
            }

            List<int> roleIDs1;
            using (var connection = SqlConnections.NewFor<ActivityRequestRow>())
            {
                roleIDs1 = connection.List<Administration.Entities.UserRoleRow>().Where(t => t.UserId == userID).Select(p => p.RoleId.Value).ToList();
            }

            if (roleIDs1 != null && roleIDs1.Count != 0)
            {
                var permissionRow = Administration.Entities.RolePermissionRow.Fields;
                List<string> permissions;
                using (var connection = SqlConnections.NewFor<Administration.Entities.RolePermissionRow>())
                {
                    permissions = connection.List<Administration.Entities.RolePermissionRow>().Where(t => roleIDs1.Contains((int)t.RoleId)).Select(p => p.PermissionKey).ToList();
                }

                foreach (string permission in permissions)
                {
                    if (permission == "Administration:Manager")
                        return View(MVC.Views.Common.Dashboard.DashboardIndex, cachedModel);
                    if (permission == "Administration:User")
                        return View(MVC.Views.Common.DashboardUser.DashboardUser_, cachedModel);
                }
            }
            else
                return View(MVC.Views.Common.Dashboard.DashboardIndex, cachedModel);

            return View(MVC.Views.Common.Dashboard.DashboardIndex, cachedModel);
        }
    }
}