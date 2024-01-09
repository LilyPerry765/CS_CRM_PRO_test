﻿
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestFinancial"), Route("{action=index}")]
    public class ActivityRequestFinancialController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestFinancial/ActivityRequestFinancialIndex.cshtml");
        }
    }
}