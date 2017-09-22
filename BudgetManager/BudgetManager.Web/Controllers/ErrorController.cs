using System.Collections.Generic;
using System.Web.Mvc;
using BudgetManager.Business;
using BudgetManager.Business.Error;
using BudgetManager.Models;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.Controllers
{
    public class ErrorController : BaseController
    {
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult List()
        {
            List<Error> errors = ErrorManager.GetList();
            return View(errors);
        }

        [HttpGet]
        public ActionResult Clear()
        {
            return View();
        }

        [HttpPost, ActionName("Clear")]
        public ActionResult ClearConfirmed()
        {
            bool deleted = ErrorManager.DeleteAll();
            ViewBag.Deleted = deleted;
            if (deleted) return RedirectToAction("List");
            return View();
        }
    }
}