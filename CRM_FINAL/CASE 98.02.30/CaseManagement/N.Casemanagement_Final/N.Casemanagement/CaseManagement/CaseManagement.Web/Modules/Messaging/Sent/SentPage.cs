



namespace CaseManagement.Messaging.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Messaging/Sent"), Route("{action=index}")]
    public class SentController : Controller
    {
        [PageAuthorize(CaseManagement.Administration.PermissionKeys.All)]
        public ActionResult Index()
        {
            return View("~/Modules/Messaging/Sent/SentIndex.cshtml");
        }
    }
}