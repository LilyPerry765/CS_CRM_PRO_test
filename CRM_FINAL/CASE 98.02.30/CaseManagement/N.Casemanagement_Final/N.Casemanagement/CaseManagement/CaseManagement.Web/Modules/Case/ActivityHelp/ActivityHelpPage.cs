
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityHelp"), Route("{action=index}")]
    public class ActivityHelpController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.ActivityProvince)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityHelp/ActivityHelpIndex.cshtml");
        }
    }
}