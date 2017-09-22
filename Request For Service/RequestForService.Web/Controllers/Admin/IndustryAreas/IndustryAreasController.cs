using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.IndustryAreas
{
	public partial class IndustryAreasController : Base.AuthenticationBaseController
	{
		private Business.Services.Admin.IndustryAreasService _business;
		private Business.Services.Admin.IndustryAreasService Business
		{
			get { return _business ?? (_business = new Business.Services.Admin.IndustryAreasService(UserId)); }
		}

		public ActionResult Index()
		{
			var model = new ViewModels.IndustryAreas.IndustryAreasListViewModel
			{
				List = Business.GetList().Entity
			};
			return View(model);
		}

		public ActionResult Add()
		{
			return View();
		}

		public ActionResult Update(Guid id)
		{
			var model = new ViewModels.IndustryAreas.IndustryAreaItemViewModel
			{
				IndustryArea = Business.GetIndustryArea(id).Entity,
			};
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