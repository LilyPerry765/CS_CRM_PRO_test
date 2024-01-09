
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SwitchDslamProvince"), Route("{action=index}")]
    public class SwitchDslamProvinceController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SwitchDslamProvince/SwitchDslamProvinceIndex.cshtml");
        }
    }
}