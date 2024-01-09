



namespace CaseManagement.Messaging.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Messaging/NewMessage"), Route("{action=index}")]
    public class NewMessageController : Controller
    {
        [PageAuthorize(CaseManagement.Administration.PermissionKeys.All)]
        public ActionResult Index()
        {
            return View("~/Modules/Messaging/NewMessage/NewMessageIndex.cshtml");
        }
    }
}