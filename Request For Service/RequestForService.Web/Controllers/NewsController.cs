using System.Web.Mvc;

namespace RequestForService.Web.Controllers
{
    public class NewsController : Base.BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}