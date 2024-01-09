
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestDelete"), Route("{action=index}")]
    public class ActivityRequestDeleteController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestDelete/ActivityRequestDeleteIndex.cshtml");
        }
    }
}