

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/Activity"), Route("{action=index}")]
    public class ActivityController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/Activity/ActivityIndex.cshtml");
        }
    }
}