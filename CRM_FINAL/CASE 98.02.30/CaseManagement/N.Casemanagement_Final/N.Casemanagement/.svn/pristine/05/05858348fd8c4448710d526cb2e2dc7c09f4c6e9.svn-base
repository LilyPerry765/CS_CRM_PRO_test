



namespace CaseManagement.WorkFlow.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("WorkFlow/WorkFlowRule"), Route("{action=index}")]
    public class WorkFlowRuleController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.Workflow)]
        public ActionResult Index()
        {
            return View("~/Modules/WorkFlow/WorkFlowRule/WorkFlowRuleIndex.cshtml");
        }
    }
}