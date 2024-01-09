
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SwitchTransitProvince"), Route("{action=index}")]
    public class SwitchTransitProvinceController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SwitchTransitProvince/SwitchTransitProvinceIndex.cshtml");
        }
    }
}