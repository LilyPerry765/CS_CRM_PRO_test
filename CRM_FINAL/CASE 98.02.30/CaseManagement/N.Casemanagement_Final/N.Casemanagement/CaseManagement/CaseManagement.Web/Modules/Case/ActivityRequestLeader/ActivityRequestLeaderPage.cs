



namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestLeader"), Route("{action=index}")]
    public class ActivityRequestLeaderController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestLeader/ActivityRequestLeaderIndex.cshtml");
        }
    }
}