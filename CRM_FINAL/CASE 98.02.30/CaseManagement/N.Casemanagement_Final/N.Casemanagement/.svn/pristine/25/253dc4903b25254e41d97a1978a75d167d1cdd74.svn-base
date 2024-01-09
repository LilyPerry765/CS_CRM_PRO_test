
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SwitchTransit"), Route("{action=index}")]
    public class SwitchTransitController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SwitchTransit/SwitchTransitIndex.cshtml");
        }
    }
}