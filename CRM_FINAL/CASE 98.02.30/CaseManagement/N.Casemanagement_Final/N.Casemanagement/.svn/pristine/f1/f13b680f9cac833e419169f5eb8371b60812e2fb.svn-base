
namespace CaseManagement.WorkFlow.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("WorkFlow/WorkFlowStatusType"), Route("{action=index}")]
    public class WorkFlowStatusTypeController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.Workflow)]
        public ActionResult Index()
        {
            return View("~/Modules/WorkFlow/WorkFlowStatusType/WorkFlowStatusTypeIndex.cshtml");
        }
    }
}