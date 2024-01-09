namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/Role"), Route("{action=index}")]
    public class RoleController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.Permission)]
        public ActionResult Index()
        {
            return View(MVC.Views.Administration.Role.RoleIndex);
        }
    }
}