
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestHistory"), Route("{action=index}")]
    public class ActivityRequestHistoryController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestHistory/ActivityRequestHistoryIndex.cshtml");
        }
    }
}