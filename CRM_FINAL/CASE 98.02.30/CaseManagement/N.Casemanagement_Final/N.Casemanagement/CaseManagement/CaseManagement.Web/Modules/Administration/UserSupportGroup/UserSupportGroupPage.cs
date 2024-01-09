

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Administration/UserSupportGroup", typeof(CaseManagement.Administration.Pages.UserSupportGroupController))]

namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/UserSupportGroup"), Route("{action=index}")]
    public class UserSupportGroupController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/UserSupportGroup/UserSupportGroupIndex.cshtml");
        }
    }
}