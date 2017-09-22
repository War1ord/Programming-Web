using System.Web.Mvc;

namespace RequestForService.Web.Controllers
{
	public class HomeController : Base.BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}