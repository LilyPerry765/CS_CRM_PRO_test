
namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/User"), Route("{action=index}")]
    public class UserController : Controller
    {
        [PageAuthorize(Administration.PermissionKeys.Permission)]
        public ActionResult Index()
        {
            return View(MVC.Views.Administration.User.UserIndex);
        }
    }
}