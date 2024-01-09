
namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/Log"), Route("{action=index}")]
    public class LogController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.Permission)]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/Log/LogIndex.cshtml");
        }
    }
}