﻿
namespace CaseManagement.StimulReport.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("StimulReport/ActivityRequestDetail"), Route("{action=index}")]
    public class ActivityRequestDetailController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/StimulReport/ActivityRequestDetail/ActivityRequestDetailIndex.cshtml");
        }
    }
}