

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/RiskLevel"), Route("{action=index}")]
    public class RiskLevelController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/RiskLevel/RiskLevelIndex.cshtml");
        }
    }
}