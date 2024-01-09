
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestDeny"), Route("{action=index}")]
    public class ActivityRequestDenyController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestDeny/ActivityRequestDenyIndex.cshtml");
        }
    }
}