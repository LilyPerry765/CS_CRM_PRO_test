
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/SwitchProvince"), Route("{action=index}")]
    public class SwitchProvinceController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/SwitchProvince/SwitchProvinceIndex.cshtml");
        }
    }
}