
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/Software"), Route("{action=index}")]
    public class SoftwareController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/Software/SoftwareIndex.cshtml");
        }
    }
}