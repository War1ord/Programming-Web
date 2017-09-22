using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.IndustryAreas
{
	public partial class IndustryAreasController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(ViewModels.IndustryAreas.IndustryAreaItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.AddIndustryArea(model.IndustryArea);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(ViewModels.IndustryAreas.IndustryAreaItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.UpdateIndustryArea(model.IndustryArea);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

	}
}