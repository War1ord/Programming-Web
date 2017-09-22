using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.IndustryLevels
{
	public partial class IndustryLevelsController : Base.AuthenticationBaseController
	{
		private Business.Services.Admin.IndustryLevelsService _business;
		private Business.Services.Admin.IndustryLevelsService Business
		{
			get { return _business ?? (_business = new Business.Services.Admin.IndustryLevelsService(UserId)); }
		}

		public ActionResult Index()
		{
			var model = new ViewModels.IndustryLevels.IndustryLevelsListViewModel
			{
				List = Business.GetList().Entity
			};
			return View(model);
		}

		public ActionResult Add()
		{
			ViewBag.IndustryAreas = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryArea>().Entity;
			return View();
		}

		public ActionResult Update(Guid id)
		{
			var model = new ViewModels.IndustryLevels.IndustryLevelItemViewModel
			{
				IndustryLevel = Business.GetEntity<RequestForService.Models.BusinessEntities.IndustryLevel>(id).Entity,
			};
			ViewBag.IndustryAreas = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryArea>().Entity;
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