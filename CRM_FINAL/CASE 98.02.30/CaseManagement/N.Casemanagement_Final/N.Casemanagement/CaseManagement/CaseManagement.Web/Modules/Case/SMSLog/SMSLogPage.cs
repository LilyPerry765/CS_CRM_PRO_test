

//[assembly:Serenity.Navigation.NavigationLink(int.MaxValue, "Case/SMSLog", typeof(CaseManagement.Case.Pages.SMSLogController))]

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SMSLog"), Route("{action=index}")]
    public class SMSLogController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.Permission)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SMSLog/SMSLogIndex.cshtml");
        }
    }
}