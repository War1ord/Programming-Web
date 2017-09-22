using RequestForService.Web.Filters;
using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers
{
	public class ErrorController : Base.BaseController
	{
		private Business.Services.Errors.ErrorLogsManager _business;
		private Business.Services.Errors.ErrorLogsManager Business
		{
			get { return _business ?? (_business = new Business.Services.Errors.ErrorLogsManager(UserId)); }
		}

		public ActionResult Index()
		{
			return View("Error");
		}

		[RequestForServiceAuthentication]
		public ActionResult List()
		{
			var model = new ViewModels.Errors.ErrorsListViewModel 
			{
				List = Business.GetEntityList(i=>i.DateCreated, true).Entity
			};
			return View(model);
		}

		[RequestForServiceAuthentication]
		public ActionResult Details(Guid id)
		{
			var model = new ViewModels.Errors.ErrorItemViewModel
			{
				Item = Business.GetEntity(id).Entity
			};
			return View(model);
		}

		[RequestForServiceAuthentication]
		public ActionResult DeleteAll()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken, ActionName("DeleteAll")]
		public ActionResult DeleteAllConfirmed()
		{
			var model = new ViewModels.Base.ViewModelBase
			{
				Result = Business.DeleteAll()
			};
			if (model.Result.IsSuccessful)
			{
				return RedirectToAction("List");
			}
			return View(model);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_business != null)
				{
					_business.Dispose();
					_business = null;
				}
			}
			base.Dispose(disposing);
		}
	}
}