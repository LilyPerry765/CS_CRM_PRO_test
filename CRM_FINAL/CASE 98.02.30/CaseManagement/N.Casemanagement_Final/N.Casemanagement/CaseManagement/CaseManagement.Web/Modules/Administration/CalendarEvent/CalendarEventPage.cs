


namespace CaseManagement.Administration.Pages
{
    using Serenity;
    using Serenity.Web;
    using System.Web.Mvc;

    [RoutePrefix("Administration/CalendarEvent"), Route("{action=index}")]
    public class CalendarEventController : Controller
    {
        [PageAuthorize("Administration")]
        public ActionResult Index()
        {
            return View("~/Modules/Administration/CalendarEvent/CalendarEventIndex.cshtml");
        }
    }
}