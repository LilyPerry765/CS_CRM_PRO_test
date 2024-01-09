
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestTechnical"), Route("{action=index}")]
    public class ActivityRequestTechnicalController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.JustRead)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestTechnical/ActivityRequestTechnicalIndex.cshtml");
        }
    }
}