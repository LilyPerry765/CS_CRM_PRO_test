﻿namespace CaseManagement.StimulReport.Pages
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
    using CaseManagement.Common;
    using CaseManagement.Case;
    using Stimulsoft.Report;
    
    using System.IO;
    using System.Text;
    using System.IO.Compression;
    using System.Runtime.Serialization.Formatters.Binary;
    using ReportTemplateDB.Common.Entities;
    using Stimulsoft.Report.Mvc;

    [Authorize, RoutePrefix("StimulReport4"), Route("{action=index}")]
    public class StimulReportPageController : Controller
    {
        #region ReportDesigner

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult Reporter()
        {
            //IDbConnection connection = new IDbConnection();

            using (var connection = SqlConnections.NewFor<ReportTemplateRow>())
            {
                Helper.SaveLog("صفحه گزارشساز", "صفحه گزارشساز", 0, " ", "", connection, Administration.ActionLog.View);
            }
            return View(MVC.Views.Common.Reporting.Reporter);
        }
         [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult GetReportTemplate()
        {

            //Stimulsoft.Base.StiLicense.LoadFromFile(@"D:\N.Casemanagement\CaseManagement\CaseManagement.Web\Modules\StimulReport\license.key");

            int reportID = 0;
            StiReport report = new StiReport();
            
            var connection = SqlConnections.NewFor<ReportTemplateRow>();
            if (string.IsNullOrEmpty(System.Web.HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReportName"]))
            {
                reportID = int.Parse(System.Web.HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReportID"].ToString());
                //using ()
                //{
                    ReportTemplateRow row = connection.List<ReportTemplateRow>().Where(t => t.Id == reportID).SingleOrDefault();
                    byte[] buffer = new byte[16 * 1024];
                    using (MemoryStream ms = new MemoryStream())
                    {
                        int read;
                        while ((read = row.Template.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            ms.Write(buffer, 0, read);
                        }
                        byte[] arr = ms.ToArray();
                        report.LoadPackedReport(arr);
                        report.ReportName = row.Title.Trim();
                    };
                //}
            }
            else
                report.ReportName = System.Web.HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReportName"].ToString();

            Helper.SaveLog("ReportTemplate ", "طراحی گزارش", 0, report.ReportName, "", connection, Administration.ActionLog.View);

            return StiMvcDesigner.GetReportTemplateResult(HttpContext, report);
        }
         [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult SaveReportTemplateAction()
        {
            
            StiReport report = StiMvcDesigner.GetReportObject(HttpContext);
            string reportName = report.ReportName.Trim();
            byte[] arr = report.SavePackedReportToByteArray();
            using (var connection = SqlConnections.NewFor<ReportTemplateRow>())
            {
                ReportTemplateRow row = new ReportTemplateRow();
                bool existsRow = connection.List<ReportTemplateRow>().Any(t => t.Title == reportName);
                if (existsRow)
                {
                    row = connection.List<ReportTemplateRow>().SingleOrDefault(t => t.Title.Trim() == reportName);
                    MemoryStream stream = new MemoryStream(arr);
                    row.Template = stream;
                    connection.UpdateById(row);
                }
                else
                {
                    int Maxrow= 0;
                    object maxRowObject = new DBHelper().ExecuteScalar("SELECT MAX(id) From ReportTemplate");
                    if (!string.IsNullOrEmpty(maxRowObject.ToString()))
                        Maxrow = int.Parse(maxRowObject.ToString());
                    MemoryStream stream = new MemoryStream(arr);
                    row.Template = stream;
                    row.Title = reportName;
                    row.Id = Maxrow + 1; //MaxID +1
                    connection.Insert(row);
                }
                Helper.SaveLog("ReportTemplate ", "طراحی گزارش", 0, reportName, "", connection, Administration.ActionLog.Update);
            }


            return StiMvcDesigner.SaveReportResult(report);
        }
        public ActionResult DesignerEvent()
        {
            //return StiMvcDesigner.DesignerEventResult(HttpContext);
            return StiMvcDesigner.DesignerEventResult();
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ReportDesigner()
        {
            return View(MVC.Views.Common.Reporting.ReportDesigner);
        }

        #endregion

        #region Report Viewer

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult MvcReportViewer()
        {
            return View(MVC.Views.Common.Reporting.MvcReportViewer);
        }

        public ActionResult ViewerEvent()
        {
            return Stimulsoft.Report.Mvc.StiMvcViewer.ViewerEventResult(HttpContext);
        }

        public ActionResult GetReport()
        {
            int reportID = int.Parse(System.Web.HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReportID"].ToString());
            StiReport report = new StiReport();
            using (var connection = SqlConnections.NewFor<ReportTemplateRow>())
            {
                ReportTemplateRow row = connection.List<ReportTemplateRow>().Where(t => t.Id == reportID).SingleOrDefault();
                byte[] buffer = new byte[16 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = row.Template.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        ms.Write(buffer, 0, read);
                    }
                    byte[] arr = ms.ToArray();
                    report.LoadPackedReport(arr);

                    //Get Data From Database
                    if (reportID == 1) // گزاش تجمیعی استان ها
                    {
                        DataTable dt = new DataTable();
                        DBHelper dbh = new DBHelper();
                        dt = dbh.ExecuteDT("EXEC usp_CollectiveProvince");
                        report.RegData("ProvianceTotalDataSource", dt);
                    }
                };
                // return StiMvcMobileDesigner.GetReportTemplateResult(HttpContext, report);
                Helper.SaveLog("مشاهده گزارش ", "مشاهده گزارش", 0, report.ReportName.Trim() , "", connection, Administration.ActionLog.View);
                return Stimulsoft.Report.Mvc.StiMvcViewer.GetReportSnapshotResult(HttpContext, report);
            } //end using
        }

        #endregion

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ProvinceLineChart()
        {
            return View(MVC.Views.StimulReport.ProvinceLineChart.ProvinceLineChart_);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult UserLeaderActivityDetail()
        {
            return View(MVC.Views.StimulReport.UserLeaderActivityDetail.UserLeaderActivityDetail_);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult UserMonthActivityDetail()
        {
            return View(MVC.Views.StimulReport.UserMonthActivityDetail.UserMonthActivityDetail_);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult UserProvinceActivityDetail()
        {
            return View(MVC.Views.StimulReport.UserProvinceActivityDetail.UserProvinceActivityDetail_);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ProvinceActivityReport()
        {
            return View(MVC.Views.StimulReport.ProvinceActivityReport.ProvinceActivityReport_);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ProvinceReport()
        {
            return View(MVC.Views.Common.Reporting.ProvinceReport);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ActivityReport()
        {
            return View(MVC.Views.Common.Reporting.ActivityReport);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult IncomFlowReport()
        {
            return View(MVC.Views.Common.Reporting.IncomeFlowReport);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult IncomFlowReportPrint()
        {
            return View(MVC.Views.Common.Reporting.IncomeFlowReportPrint);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ActivityReportPrint()
        {
            return View(MVC.Views.Common.Reporting.ActivityReportPrint);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult ProvinceReportPrint()
        {
            return View(MVC.Views.Common.Reporting.ProvinceReportPrint);
        }


        [PageAuthorize(Case.PermissionKeys.Report)]
        [HttpGet]
        public ActionResult GetProvinceReportInfo(string incomFlowID, string cycleID, string activityID, string fromDate, string toDate)
        {
            List<ProvinceReportInfo> reportList = new List<ProvinceReportInfo>();

            DateTime? date1 = new DateTime();
            if (!string.IsNullOrEmpty(fromDate))
                date1 = Helper.PersianToGregorian(fromDate);

            DateTime? date2 = new DateTime();
            if (!string.IsNullOrEmpty(toDate))
            {
                date2 = Helper.PersianToGregorian(toDate);
                date2 = date2.Value.AddDays(1);
            }
            using (var connection = SqlConnections.NewFor<ActivityRequestRow>())
            {
                reportList = connection.List<ActivityRequestRow>().Where(t => (incomFlowID == "0" || t.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                  (cycleID == "0" || t.CycleId == Convert.ToInt32(cycleID)) &&
                                                                  (activityID == "0" || t.ActivityId == Convert.ToInt32(activityID)) &&
                                                                  (fromDate == "" || t.DiscoverLeakDate >= date1) &&
                                                                  (toDate == "" || t.DiscoverLeakDate < date2) &&
                                                                  (t.IsDeleted == false) && (t.EndDate != null))
                                                                  .GroupBy(x => x.ProvinceId)
                                                                  .Select(y => new ProvinceReportInfo
                                                                  {
                                                                      ProvinceID = (int)y.Key,
                                                                      ProvinceName = connection.List<ProvinceRow>().Where(t => t.Id == y.Key.Value).Select(t => t.Name).SingleOrDefault(),
                                                                      SumCycleCost = y.Sum(x => x.CycleCost),
                                                                      SumDelayedCost = y.Sum(x => x.DelayedCost),
                                                                      SumTotalLeakage = y.Sum(x => x.TotalLeakage),
                                                                      SumRecoverableLeakage = y.Sum(x => x.RecoverableLeakage),
                                                                      SumRecovered = y.Sum(x => x.Recovered),
                                                                     // ActivityCount = connection.List<ActivityRequestRow>().Where(r =>
                                                                     //     (incomFlowID == "0" || r.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                     //     (cycleID == "0" || r.CycleId == Convert.ToInt32(cycleID)) &&
                                                                     //     (activityID == "0" || r.ActivityId == Convert.ToInt32(activityID)) &&
                                                                     //     (fromDate == "" || r.DiscoverLeakDate >= date1) &&
                                                                     //     (toDate == "" || r.DiscoverLeakDate < date2) &&
                                                                     //     (r.IsDeleted == false) && (r.EndDate != null) &&
                                                                     //     (r.ProvinceId == y.Key)).Count(),
                                                                      TechnicalConfirmCount = connection.List<ActivityRequestRow>().Where(r =>
                                                                          (incomFlowID == "0" || r.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                          (cycleID == "0" || r.CycleId == Convert.ToInt32(cycleID)) &&
                                                                          (activityID == "0" || r.ActivityId == Convert.ToInt32(activityID)) &&
                                                                          (fromDate == "" || r.DiscoverLeakDate >= date1) &&
                                                                          (toDate == "" || r.DiscoverLeakDate < date2) &&
                                                                          (r.IsDeleted == false) && (r.EndDate == null) && (r.ConfirmTypeID == ConfirmType.Financial) &&
                                                                          (r.ProvinceId == y.Key)).Count(),
                                                                      FinancialConfirmCount = connection.List<ActivityRequestRow>().Where(r =>
                                                                          (incomFlowID == "0" || r.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                          (cycleID == "0" || r.CycleId == Convert.ToInt32(cycleID)) &&
                                                                          (activityID == "0" || r.ActivityId == Convert.ToInt32(activityID)) &&
                                                                          (fromDate == "" || r.DiscoverLeakDate >= date1) &&
                                                                          (toDate == "" || r.DiscoverLeakDate < date2) &&
                                                                          (r.IsDeleted == false) && (r.EndDate != null) && (r.ConfirmTypeID == ConfirmType.Financial) &&
                                                                          (r.ProvinceId == y.Key)).Count()

                                                                  }).Distinct().ToList();
            }

            return Json(reportList, JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        [HttpGet]
        public ActionResult GetActivityReportInfo(string incomFlowID, string cycleID, string provinceID, string fromDate, string toDate)
        {
            List<ActivityReportInfo> reportList = new List<ActivityReportInfo>();

            DateTime? date1 = new DateTime();
            if (!string.IsNullOrEmpty(fromDate))
                date1 = Helper.PersianToGregorian(fromDate);

            DateTime? date2 = new DateTime();
            if (!string.IsNullOrEmpty(toDate))
            {
                date2 = Helper.PersianToGregorian(toDate);
                date2 = date2.Value.AddDays(1);
            }
            using (var connection = SqlConnections.NewFor<ActivityRequestRow>())
            {
                reportList = connection.List<ActivityRequestRow>().Where(t => (incomFlowID == "0" || t.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                  (cycleID == "0" || t.CycleId == Convert.ToInt32(cycleID)) &&
                                                                  (provinceID == "0" || t.ProvinceId == Convert.ToInt32(provinceID)) &&
                                                                  (fromDate == "" || t.DiscoverLeakDate >= date1) &&
                                                                  (toDate == "" || t.DiscoverLeakDate < date2) &&
                                                                  (t.IsDeleted == false) && (t.EndDate != null))
                                                                  .GroupBy(x => x.ActivityId)
                                                                  .Select(y => new ActivityReportInfo
                                                                  {
                                                                      ActivityID = (int)y.Key,
                                                                      ActivityCode = connection.List<ActivityRow>().Where(t => t.Id == y.Key.Value).Select(t => t.Code).SingleOrDefault(),
                                                                      SumCycleCost = y.Sum(x => x.CycleCost),
                                                                      SumDelayedCost = y.Sum(x => x.DelayedCost),
                                                                      SumTotalLeakage = y.Sum(x => x.TotalLeakage),
                                                                      SumRecoverableLeakage = y.Sum(x => x.RecoverableLeakage),
                                                                      SumRecovered = y.Sum(x => x.Recovered),
                                                                      ActivityCount = connection.List<ActivityRequestRow>().Where(r =>
                                                                          (incomFlowID == "0" || r.IncomeFlowId == Convert.ToInt32(incomFlowID)) &&
                                                                          (cycleID == "0" || r.CycleId == Convert.ToInt32(cycleID)) &&
                                                                          (provinceID == "0" || r.ProvinceId == Convert.ToInt32(provinceID)) &&
                                                                          (fromDate == "" || r.DiscoverLeakDate >= date1) &&
                                                                          (toDate == "" || r.DiscoverLeakDate < date2) &&
                                                                          (r.IsDeleted == false) && (r.EndDate != null) &&
                                                                          (r.ActivityId == y.Key)).Count()

                                                                  }).Distinct().ToList();
            }

            return Json(reportList, JsonRequestBehavior.AllowGet);
        }

        [PageAuthorize(Case.PermissionKeys.Report)]
        [HttpGet]
        public ActionResult GetIncomeFlowReportInfo(string activityID, string cycleID, string provinceID, string fromDate, string toDate)
        {
            List<IncomFlowReportInfo> reportList = new List<IncomFlowReportInfo>();

            DateTime? date1 = new DateTime();
            if (!string.IsNullOrEmpty(fromDate))
                date1 = Helper.PersianToGregorian(fromDate);

            DateTime? date2 = new DateTime();
            if (!string.IsNullOrEmpty(toDate))
            {
                date2 = Helper.PersianToGregorian(toDate);
                date2 = date2.Value.AddDays(1);
            }
            using (var connection = SqlConnections.NewFor<ActivityRequestRow>())
            {
                reportList = connection.List<ActivityRequestRow>().Where(t => (activityID == "0" || t.ActivityId == Convert.ToInt32(activityID)) &&
                                                                  (cycleID == "0" || t.CycleId == Convert.ToInt32(cycleID)) &&
                                                                  (provinceID == "0" || t.ProvinceId == Convert.ToInt32(provinceID)) &&
                                                                  (fromDate == "" || t.DiscoverLeakDate >= date1) &&
                                                                  (toDate == "" || t.DiscoverLeakDate < date2) &&
                                                                  (t.IsDeleted == false) && (t.EndDate != null))
                                                                  .GroupBy(x => x.IncomeFlowId)
                                                                  .Select(y => new IncomFlowReportInfo
                                                                  {
                                                                      IncomeFlowID = (int)y.Key,
                                                                      IncomeFlowName = connection.List<IncomeFlowRow>().Where(t => t.Id == y.Key.Value).Select(t => t.Name).SingleOrDefault(),
                                                                      SumCycleCost = y.Sum(x => x.CycleCost),
                                                                      SumDelayedCost = y.Sum(x => x.DelayedCost),
                                                                      SumTotalLeakage = y.Sum(x => x.TotalLeakage),
                                                                      SumRecoverableLeakage = y.Sum(x => x.RecoverableLeakage),
                                                                      SumRecovered = y.Sum(x => x.Recovered),
                                                                      ActivityCount = connection.List<ActivityRequestRow>().Where(r =>
                                                                                  (activityID == "0" || r.ActivityId == Convert.ToInt32(activityID)) &&
                                                                                  (cycleID == "0" || r.CycleId == Convert.ToInt32(cycleID)) &&
                                                                                  (provinceID == "0" || r.ProvinceId == Convert.ToInt32(provinceID)) &&
                                                                                  (fromDate == "" || r.DiscoverLeakDate >= date1) &&
                                                                                  (toDate == "" || r.DiscoverLeakDate < date2) &&
                                                                                  (r.IsDeleted == false) && (r.EndDate != null) &&
                                                                                  (r.IncomeFlowId == y.Key)).Count()

                                                                  }).Distinct().ToList();
            }

            return Json(reportList, JsonRequestBehavior.AllowGet);
        }
    }
}