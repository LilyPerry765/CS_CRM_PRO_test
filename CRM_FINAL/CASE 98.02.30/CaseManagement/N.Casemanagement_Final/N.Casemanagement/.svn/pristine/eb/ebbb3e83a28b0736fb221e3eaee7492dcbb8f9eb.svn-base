
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestConfirm"), Route("{action=index}")]
    public class ActivityRequestConfirmController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.User)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestConfirm/ActivityRequestConfirmIndex.cshtml");
        }
    }
}