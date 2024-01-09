

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Administration/NotificationGroup", typeof(CaseManagement.Administration.Pages.NotificationGroupController))]

namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/NotificationGroup"), Route("{action=index}")]
    public class NotificationGroupController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/NotificationGroup/NotificationGroupIndex.cshtml");
        }
    }
}