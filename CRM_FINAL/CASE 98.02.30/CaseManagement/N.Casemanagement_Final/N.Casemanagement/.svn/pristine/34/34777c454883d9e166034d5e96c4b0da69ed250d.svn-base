



namespace CaseManagement.Case.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Case/RepeatTerm"), Route("{action=index}")]
    public class RepeatTermController : Controller
    {
        [PageAuthorize(Case.PermissionKeys.BasicInfo)]
        public ActionResult Index()
        {
            return View("~/Modules/Case/RepeatTerm/RepeatTermIndex.cshtml");
        }
    }
}