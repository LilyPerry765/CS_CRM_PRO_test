

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityGroup"), Route("{action=index}")]
    public class ActivityGroupController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityGroup/ActivityGroupIndex.cshtml");
        }
    }
}