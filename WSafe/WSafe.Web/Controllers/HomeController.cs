using System.Web.Mvc;
using WSafe.Web.Filters;

namespace WSafe.Web.Controllers
{
    public class HomeController : Controller
    {
        [AuthorizeUser(roleID: 1, operationID: 1)]
        public ActionResult Index()
        {
            return View();
        }
        [AuthorizeUser(roleID: 2, operationID: 2)]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AuthorizeUser(roleID: 3, operationID: 3)]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Logout()
        {
            Session["User"] = null;
            return null;
        }
    }
}