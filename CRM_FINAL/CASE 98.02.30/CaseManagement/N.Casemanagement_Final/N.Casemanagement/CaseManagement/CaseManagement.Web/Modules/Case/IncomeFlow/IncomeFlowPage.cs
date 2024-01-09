

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/IncomeFlow"), Route("{action=index}")]
    public class IncomeFlowController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/IncomeFlow/IncomeFlowIndex.cshtml");
        }
    }
}