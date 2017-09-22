using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Admin.HourlyRates
{
    public partial class HourlyRatesController
    {
		[HttpPost]
	    public ActionResult Add(ViewModels.HourlyRates.HourlyRateItemViewModel model)
	    {
			if (ModelState.IsValid)
			{
				model.Result = Business.CreateEntity(model.Item, session.User.BusinessEntityId);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

		[HttpPost]
		public ActionResult Update(ViewModels.HourlyRates.HourlyRateItemViewModel model)
	    {
			if (ModelState.IsValid)
			{
				model.Result = Business.UpdateEntityProperties(model.Item);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Index");
				}
			}
			return View(model);
		}

    }
}