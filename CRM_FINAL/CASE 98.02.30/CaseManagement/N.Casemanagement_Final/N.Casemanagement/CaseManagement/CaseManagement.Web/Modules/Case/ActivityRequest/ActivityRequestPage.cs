﻿
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequest"), Route("{action=index}")]
    public class ActivityRequestController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequest/ActivityRequestIndex.cshtml");
        }
    }
}