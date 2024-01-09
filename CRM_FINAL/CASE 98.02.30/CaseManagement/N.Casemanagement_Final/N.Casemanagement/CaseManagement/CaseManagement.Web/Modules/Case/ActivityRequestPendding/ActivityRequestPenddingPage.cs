
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestPendding"), Route("{action=index}")]
    public class ActivityRequestPenddingController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestPendding/ActivityRequestPenddingIndex.cshtml");
        }
    }
}