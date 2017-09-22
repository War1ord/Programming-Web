using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.IndustryLevels
{
	public partial class IndustryLevelsController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(ViewModels.IndustryLevels.IndustryLevelItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.CreateIndustryLevel(model.IndustryLevel, session.User.Id);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			ViewBag.IndustryAreas = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryArea>().Entity;
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(ViewModels.IndustryLevels.IndustryLevelItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.UpdateIndustryLevel(model.IndustryLevel);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			ViewBag.IndustryAreas = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryArea>().Entity;
			return View(model);
		}
	}
}