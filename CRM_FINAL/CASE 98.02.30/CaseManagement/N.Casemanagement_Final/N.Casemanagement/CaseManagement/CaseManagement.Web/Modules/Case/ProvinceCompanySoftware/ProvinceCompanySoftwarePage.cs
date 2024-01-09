
namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/ProvinceCompanySoftware"), Route("{action=index}")]
    public class ProvinceCompanySoftwareController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/ProvinceCompanySoftware/ProvinceCompanySoftwareIndex.cshtml");
        }
    }
}