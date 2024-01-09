﻿
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ProvinceProgram"), Route("{action=index}")]
    public class ProvinceProgramController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ProvinceProgram/ProvinceProgramIndex.cshtml");
        }

    }
}