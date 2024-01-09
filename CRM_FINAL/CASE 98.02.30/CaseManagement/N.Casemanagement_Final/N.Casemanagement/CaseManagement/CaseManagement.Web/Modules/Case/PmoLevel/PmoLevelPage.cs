

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/PmoLevel"), Route("{action=index}")]
    public class PmoLevelController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/PmoLevel/PmoLevelIndex.cshtml");
        }
    }
}