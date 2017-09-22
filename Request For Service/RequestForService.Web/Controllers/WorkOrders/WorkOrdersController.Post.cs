using RequestForService.Models.BusinessEntities;
using RequestForService.Models.WorkOrders;
using System.Web.Mvc;
using RequestForService.Web.ViewModels.WorkOrders;

namespace RequestForService.Web.Controllers.WorkOrders
{
	public partial class WorkOrdersController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Create(WorkOrder_ViewModel model)
		{
			if (ModelState.IsValid)
			{
			    var result = Service.Create(model.WorkOrder);
			    model.Result = result;
				if (model.Result.IsSuccessful)
				{
                    return RedirectToAction("Details", new { id = result.Entity });
				}
			}
			model.WorkOrderTypes = Service.GetEntityDisplayList<WorkOrderType, string>(i => new Business.Models.EntityDisplay { Id = i.Id, Value = i.Name }, i => i.Name).Entity;
			model.BusinessEntities = Service.GetEntityDisplayList<BusinessEntity, string>(i => new Business.Models.EntityDisplay { Id = i.Id, Value = i.Name }, i => i.Name).Entity;
			return View(model);
		}

	}
}