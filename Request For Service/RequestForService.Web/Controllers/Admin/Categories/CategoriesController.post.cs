using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.Categories
{
	public partial class CategoriesController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(ViewModels.Categories.CategoryItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.Add(model.Category, session.User.BusinessEntityId);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(ViewModels.Categories.CategoryItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.Update(model.Category);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

	}
}