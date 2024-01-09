
namespace CaseManagement.WorkFlow.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("WorkFlow/WorkFlowAction"), Route("{action=index}")]
    public class WorkFlowActionController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/WorkFlow/WorkFlowAction/WorkFlowActionIndex.cshtml");
        }
    }
}