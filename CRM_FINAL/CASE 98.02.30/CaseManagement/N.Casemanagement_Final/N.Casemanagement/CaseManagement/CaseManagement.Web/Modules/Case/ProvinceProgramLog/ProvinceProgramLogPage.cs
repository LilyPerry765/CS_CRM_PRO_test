
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ProvinceProgramLog"), Route("{action=index}")]
    public class ProvinceProgramLogController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ProvinceProgramLog/ProvinceProgramLogIndex.cshtml");
        }
    }
}