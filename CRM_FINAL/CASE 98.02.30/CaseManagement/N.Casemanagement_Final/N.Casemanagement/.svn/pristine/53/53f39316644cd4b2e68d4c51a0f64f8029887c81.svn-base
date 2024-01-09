
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestConfirmAdmin"), Route("{action=index}")]
    public class ActivityRequestConfirmAdminController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.Manager)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestConfirmAdmin/ActivityRequestConfirmAdminIndex.cshtml");
        }
    }
}