﻿using CaseManagement.Administration.Entities;
using CaseManagement.Case;
using CaseManagement.Case.Entities;
//using CaseManagement.Common.ServerStatus;
using Serenity;
using Serenity.Data;
using Serenity.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Management;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace CaseManagement.Common.Pages
{

    [Authorize, RoutePrefix("Common"), Route("{action=index}")]
    public class CommonPageController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.User)]
        public ActionResult DashboardUser()
        {
            return View(MVC.Views.Common.DashboardUser.DashboardUser_);
            
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult DashboardUser1()
        {
            return View(MVC.Views.Common.DashboardUser.DashboardUser_);
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult Dashboard96()
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

                        DateTime startDate = new DateTime(2017, 3, 21);
                        DateTime endYearDate = new DateTime(2018, 3, 21);

                        if (provinceID != null)
                        {
                            model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                            model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                            model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate != null && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false).Count();

                            model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false && t.EndDate == null).Count();
                        }
                        else
                        {
                            if (statusIDs != null)
                            {
                                model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                                model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                                model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate != null && t.IsDeleted == false).Count();

                                model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.IsDeleted == false && t.EndDate == null).Count();
                            }
                        }
                    }

                    return model;
                });


            int userID = int.Parse(Serenity.Authorization.UserId);

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
                        return View(MVC.Views.Common.Dashboard.Dashboard96, cachedModel);
                    if (permission == "Administration:User")
                        return View(MVC.Views.Common.DashboardUser.DashboardUser96, cachedModel);
                }
            }
            else
                return View(MVC.Views.Common.Dashboard.Dashboard96, cachedModel);

            return View(MVC.Views.Common.Dashboard.Dashboard96, cachedModel);
        }

        [PageAuthorize(Administration.PermissionKeys.User)]
        public ActionResult DashboardUser96()
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

                        DateTime startDate = new DateTime(2017, 3, 21);
                        DateTime endYearDate = new DateTime(2018, 3, 21);

                        if (provinceID != null)
                        {
                            model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                            model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && t.ProvinceId != null && t.ProvinceId == provinceID && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                            model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate != null && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false).Count();

                            model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.ProvinceId != null && t.ProvinceId == provinceID && t.IsDeleted == false && t.EndDate == null).Count();
                        }
                        else
                        {
                            if (statusIDs != null)
                            {
                                model.TechnicalRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Technical && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                                model.FinancialRequestCount = connection.List<ActivityRequestRow>().Where(t => t.ConfirmTypeID == ConfirmType.Financial && t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate == null && statusIDs.Contains((int)t.StatusID) && t.IsDeleted == false).Count();

                                model.ConfirmRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.EndDate != null && t.IsDeleted == false).Count();

                                model.PenddingRequestCount = connection.List<ActivityRequestRow>().Where(t => t.DiscoverLeakDate >= startDate && t.DiscoverLeakDate < endYearDate && t.IsDeleted == false && t.EndDate == null).Count();
                            }
                        }
                    }

                    return model;
                });

            int userID = int.Parse(Serenity.Authorization.UserId);

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
                        return View(MVC.Views.Common.DashboardUser.DashboardUser96, cachedModel);
                    if (permission == "Administration:User")
                        return View(MVC.Views.Common.DashboardUser.DashboardUser96, cachedModel);
                }
            }
            else
                return View(MVC.Views.Common.DashboardUser.DashboardUser96, cachedModel);

            return View(MVC.Views.Common.DashboardUser.DashboardUser96, cachedModel);
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult ServerStatus()
        {
            return View(MVC.Views.Common.ServerStatus.ServerStatus_);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ProvinceProgramPrint()
        {
            return View(MVC.Views.Common.Reporting.ProvinceProgramPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
       // [PageAuthorize("Administration")]
        public ActionResult ActivityRequestConfirmPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestConfirmPrint);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        // [PageAuthorize("Administration")]
        public ActionResult LogPrint()
        {
            return View(MVC.Views.Common.Reporting.LogPrint);
        }
        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestReportPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestReportPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestDetailPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestDetailPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestTechnicalPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestTechnicalPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestFinancialPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestFinancialPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestDenyPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestDenyPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestPendingPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestPendingPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestLeaderPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestLeaderPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestPendingInfoTOPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestPendingInfoPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.General)]
        public ActionResult ActivityRequestConfirmedInfoTOPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityRequestConfirmedInfoPrint);
        }

        [PageAuthorize(Administration.PermissionKeys.All)]
        public ActionResult Notifications()
        {
            return View(MVC.Views.Common.Notification.Notifications);
        }

        [PageAuthorize(Administration.PermissionKeys.All)]
        public ActionResult NotificationUserGroupsList()
        {
            return View(MVC.Views.Common.Notification.UserGroupsList);
        }


        [PageAuthorize(Administration.PermissionKeys.User)]
        public ActionResult Document()
        {
            return View(MVC.Views.Common.Document.Document_);
        }

       

        public string getUptime()
        {
            string[] Uptime = new string[2];
            TimeSpan _uptime;

            var uptime = new PerformanceCounter("System", "System Up Time");

            uptime.NextValue();
            Thread.Sleep(1000);

            _uptime = TimeSpan.FromSeconds(uptime.NextValue());
            Uptime = _uptime.ToString().Split('.');

            return Uptime[0];
            //return Json(Uptime[0]);
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult getRamUsage()
        {
            string text = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\a.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                text = sr.ReadToEnd();
            }

            String[] substrings = text.Split(',');

            var obj = new Memory
            {
                Free = substrings[2],
                Used = substrings[1],
                TotalMemory = substrings[0]
            };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult getRamUsed()
        {
            string text = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\a.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                text = sr.ReadToEnd();
            }

            String[] substrings = text.Split(',');

            return Json(substrings[1], JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Administration.PermissionKeys.Manager)]
        [HttpGet]
        public ActionResult getCPULoadAvg()
        {
            string text = "";
            using (System.IO.StreamReader sr = new System.IO.StreamReader(@"D:\a.txt"))
            {
                // Read the stream to a string, and write the string to the console.
                text = sr.ReadToEnd();
            }

            String[] substrings = text.Split(',');

            return Json(substrings[4], JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Administration.PermissionKeys.All)]
        [HttpGet]
        public ActionResult UserAttributes()
        {
            string DB_ProvinceName;
            string DB_ImagePath;
            UserRow User = null;
            using (var connection = SqlConnections.NewFor<UserRow>())
            {

                User = connection.List<UserRow>().Where(t => t.UserId == Int32.Parse(Authorization.UserDefinition.Id)).SingleOrDefault();
                DB_ImagePath = User.ImagePath;
            }

            using (var connection = SqlConnections.NewFor<ProvinceRow>())
            {
                ProvinceRow Province = null;
                if (User.ProvinceId != null)
                {
                    Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                    DB_ProvinceName = Province.Name;
                }
                else
                {
                    //Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                    DB_ProvinceName = " ";
                }
            }

            var obj = new User
            {
                Id = Authorization.UserDefinition.Id,

                Name = Authorization.UserDefinition.DisplayName,

                ImagePath = DB_ImagePath,

                ProvinceName = DB_ProvinceName

            };
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Administration.PermissionKeys.All)]
        [HttpGet]
        public ActionResult UserGroups()
        {
            var fieldsUserSupportGroup = UserRow.Fields;

            List<UserSupportGroupRow> userSupportGroups = null;
            List<GroupItem> groupNames = null;
            using (var connection = SqlConnections.NewFor<UserSupportGroupRow>())
            {
                // retrieve the support groups to which the user belongs
                userSupportGroups = connection.List<UserSupportGroupRow>(new Criteria(fieldsUserSupportGroup.UserId) == Authorization.UserDefinition.Id).ToList();

                List<int> groupIDs = connection.List<UserSupportGroupRow>().Where(t => t.UserId == Convert.ToInt32(Authorization.UserDefinition.Id)).Select(t => (int)t.GroupId).ToList();

                groupNames = connection.List<NotificationGroupRow>().Where(t => groupIDs.Contains((int)t.Id)).Select(t => new GroupItem() { GroupId = (int)t.Id, GroupName = t.Name }).ToList();
            }

            return Json(groupNames, JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Administration.PermissionKeys.All)]
        [HttpGet]
        public ActionResult NotificationList(string GroupId)
        {

            var fieldsNotifications = NotificationRow.Fields;

            List<NotificationRow> Notifications = null;
            using (var connection = SqlConnections.NewFor<NotificationRow>())
            {
                // retrieve the support groups to which the user belongs

                // if(Int32.Parse(GroupId) !=null)
                Notifications = connection.List<NotificationRow>().Where(t => t.GroupId == Int32.Parse(GroupId)).ToList();

            }
            List<NotificationItem> list = new List<NotificationItem>();

            NotificationItem Item = null;
            if (Notifications != null)
            {
                foreach (NotificationRow currentNotification in Notifications)
                {
                    Item = new NotificationItem();
                    Item.Id = (int)currentNotification.Id;

                    UserRow User = null;
                    using (var connection = SqlConnections.NewFor<UserRow>())
                    {

                        User = connection.List<UserRow>().Where(t => t.UserId == currentNotification.UserId).SingleOrDefault();
                        Item.ImagePath = User.ImagePath;
                        Item.UserName = User.DisplayName;
                    }
                    using (var connection = SqlConnections.NewFor<ProvinceRow>())
                    {
                        ProvinceRow Province = null;
                        if (User.ProvinceId != null)
                        {
                            Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                            Item.ProvinceName = Province.Name;
                        }
                        else
                        {
                            //Province = connection.List<ProvinceRow>().Where(t => t.Id == User.ProvinceId).SingleOrDefault();
                            Item.ProvinceName = " ";
                        }
                    }


                    Item.GroupId = currentNotification.GroupId.ToString();
                    Item.Message = currentNotification.Message;

                    Item.InsertDate = CaseManagement.Case.Helper.GetPersianDate(currentNotification.InsertDate, Case.Helper.DateStringType.Compelete);

                    list.Add(Item);
                }
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }


    }
    public class Memory
    {
        public string Free;
        public string Used;
        public string TotalMemory;
    }

    public class User
    {
        public string Id;
        public string Name;
        public string ImagePath;
        public string ProvinceName;
    }

    public class NotificationItem
    {
        public int Id;
        public string ImagePath;
        public string UserName;
        public string ProvinceName;
        public string GroupId;
        public string Message;
        public string InsertDate;
    }

    public class GroupItem
    {
        public int GroupId;
        public string GroupName;
    }
}