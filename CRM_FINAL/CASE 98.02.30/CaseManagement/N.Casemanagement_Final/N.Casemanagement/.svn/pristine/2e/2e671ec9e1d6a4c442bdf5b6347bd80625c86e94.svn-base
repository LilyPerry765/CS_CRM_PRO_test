
namespace CaseManagement.WorkFlow.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("WorkFlow/WorkFlowStatus"), Route("{action=index}")]
    public class WorkFlowStatusController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.Workflow)]
        public ActionResult Index()
        {
            return View("~/Modules/WorkFlow/WorkFlowStatus/WorkFlowStatusIndex.cshtml");
        }
    }
}