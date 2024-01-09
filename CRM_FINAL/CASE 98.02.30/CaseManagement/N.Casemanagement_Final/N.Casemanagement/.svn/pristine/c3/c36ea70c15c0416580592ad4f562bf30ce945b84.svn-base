

namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/CustomerEffect"), Route("{action=index}")]
    public class CustomerEffectController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/CustomerEffect/CustomerEffectIndex.cshtml");
        }
    }
}