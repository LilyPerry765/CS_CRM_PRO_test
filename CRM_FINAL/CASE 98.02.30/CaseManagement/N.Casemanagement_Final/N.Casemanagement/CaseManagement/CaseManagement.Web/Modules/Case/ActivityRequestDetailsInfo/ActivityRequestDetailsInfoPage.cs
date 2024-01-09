

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Case/ActivityRequestDetailsInfo", typeof(CaseManagement.Case.Pages.ActivityRequestDetailsInfoController))]

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestDetailsInfo"), Route("{action=index}")]
    public class ActivityRequestDetailsInfoController : Controller
    {
        [PageAuthorize(CaseManagement.Administration.PermissionKeys.All)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestDetailsInfo/ActivityRequestDetailsInfoIndex.cshtml");
        }
    }
}