using Serenity;
using Serenity.Web;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Stimulsoft;
using Stimulsoft.Report;

using Stimulsoft.Base;



namespace CaseManagement.StimulReport.Pages
{

    [RoutePrefix("StimulReport1"), Route("{action=index}")]
    public class ReportController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.Report)]
        public ActionResult Index()
        {
            return View("~/Modules/StimulReport/ReportIndex.cshtml");            
        }

        //[PageAuthorize(Case.PermissionKeys.Report)]
        //public ActionResult GetReportSnapshot()
        //{
        //    StiReport report = new StiReport();
        //    report.Load(Server.MapPath("~/Content/SimpleList.mrt"));

        //    return StiMvcViewer.GetReportSnapshotResult(report);
        //}

        //public ActionResult ViewerEvent()
        //{
        //    return StiMvcViewer.ViewerEventResult();
        //}

        //public FileResult ExportReport()
        //{
        //    StiReport report = StiMvcViewer.GetReportObject();

        //    // Some code with report before export

        //    return StiMvcViewer.ExportReportResult(report);
        //}
    }
}