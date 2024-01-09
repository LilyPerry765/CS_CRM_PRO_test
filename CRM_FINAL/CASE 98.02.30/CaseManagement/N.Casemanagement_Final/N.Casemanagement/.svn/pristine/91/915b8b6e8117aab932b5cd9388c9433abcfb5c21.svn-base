

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Administration/Notification", typeof(CaseManagement.Administration.Pages.NotificationController))]

namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/Notification"), Route("{action=index}")]
    public class NotificationController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/Notification/NotificationIndex.cshtml");
        }
    }
}