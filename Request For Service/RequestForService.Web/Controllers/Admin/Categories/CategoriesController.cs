using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.Categories
{
	public partial class CategoriesController : Base.AuthenticationBaseController
	{
		private Business.Services.Admin.CategoriesService _business;
		private Business.Services.Admin.CategoriesService Business
		{
			get { return _business ?? (_business = new Business.Services.Admin.CategoriesService(UserId)); }
		}

		public ViewResult Index()
		{
			var model = new ViewModels.Categories.CategoriesListViewModel
			{
				List = Business.GetCategories(session.User.BusinessEntityId).Entity
			};
			return View(model);
		}

		public ViewResult Add()
		{
			return View();
		}

		public ViewResult Update(Guid id)
		{
			var model = new ViewModels.Categories.CategoryItemViewModel
			{
				Category = Business.GetCategory(id).Entity
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