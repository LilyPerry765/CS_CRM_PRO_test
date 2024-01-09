
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ActivityRequestCommentReason"), Route("{action=index}")]
    public class ActivityRequestCommentReasonController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ActivityRequestCommentReason/ActivityRequestCommentReasonIndex.cshtml");
        }
    }
}