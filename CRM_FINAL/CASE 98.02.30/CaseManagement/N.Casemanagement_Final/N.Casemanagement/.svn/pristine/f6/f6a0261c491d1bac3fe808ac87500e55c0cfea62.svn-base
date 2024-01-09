
namespace CaseManagement.StimulReport.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("StimulReport/ActivityRequestReport"), Route("{action=index}")]
    public class ActivityRequestReportController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/StimulReport/ActivityRequestReport/ActivityRequestReportIndex.cshtml");
        }
    }
}