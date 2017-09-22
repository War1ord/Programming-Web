using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.BusinessEntities
{
	public partial class BusinessEntitiesController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(ViewModels.BusinessEntities.BusinessEntityItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.AddBusinessEntity(model.BusinessEntity);
				if (model.Result.IsSuccessful)
				{
					ReloadUser();
					return RedirectToAction("Index", new { id = model.ParentEntityId });
				}
			}
			ViewBag.IndustryLevelList = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryLevel, string>(
				i => i.Name, false).Entity;
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(ViewModels.BusinessEntities.BusinessEntityItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.UpdateBusinessEntity(model.BusinessEntity);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index", new {id = model.ParentEntityId});
				}
			}
			ViewBag.IndustryLevelList = Business.GetEntityList<RequestForService.Models.BusinessEntities.IndustryLevel, string>(
				i => i.Name, isSortDescending: false).Entity;
			return View(model);
		}
	}
}