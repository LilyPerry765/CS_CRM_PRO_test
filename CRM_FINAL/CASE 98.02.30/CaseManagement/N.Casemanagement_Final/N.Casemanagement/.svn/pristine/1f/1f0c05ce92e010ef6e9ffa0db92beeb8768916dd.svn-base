
namespace CaseManagement.WorkFlow.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("WorkFlow/WorkFlowStep"), Route("{action=index}")]
    public class WorkFlowStepController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.Workflow)]
        public ActionResult Index()
        {
            return View("~/Modules/WorkFlow/WorkFlowStep/WorkFlowStepIndex.cshtml");
        }
    }
}