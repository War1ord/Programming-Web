using RequestForService.Web.ViewModels.WorkOrderTypes;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.WorkOrderTypes
{
	public partial class WorkOrderTypesController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Add(WorkOrderTypeItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.CreateType(model.Item, session.User.BusinessEntityId);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Update(WorkOrderTypeItemViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.UpdateNameAndDescription(model.Item);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}
	}
}