
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SwitchDslam"), Route("{action=index}")]
    public class SwitchDslamController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SwitchDslam/SwitchDslamIndex.cshtml");
        }
    }
}