



namespace CaseManagement.Messaging.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Messaging/Inbox"), Route("{action=index}")]
    public class InboxController : Controller
    {
        [PageAuthorize(CaseManagement.Administration.PermissionKeys.All)]
        public ActionResult Index()
        {
            return View("~/Modules/Messaging/Inbox/InboxIndex.cshtml");
        }
    }
}