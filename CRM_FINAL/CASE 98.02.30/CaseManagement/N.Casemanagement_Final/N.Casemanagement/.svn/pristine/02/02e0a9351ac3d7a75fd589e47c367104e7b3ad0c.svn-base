

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/Cycle"), Route("{action=index}")]
    public class CycleController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/Cycle/CycleIndex.cshtml");
        }
    }
}