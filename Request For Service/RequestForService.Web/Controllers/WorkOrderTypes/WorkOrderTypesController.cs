using RequestForService.Models.WorkOrders;
using System;
using System.Web.Mvc;

namespace RequestForService.Web.Controllers.WorkOrderTypes
{
	public partial class WorkOrderTypesController : Base.AuthenticationBaseController
	{
		private Business.Services.WorkOrders.WorkOrderTypeService _business;
		private Business.Services.WorkOrders.WorkOrderTypeService Business
		{
			get { return _business ?? (_business = new Business.Services.WorkOrders.WorkOrderTypeService(UserId)); }
		}

		public ActionResult Index()
		{
			var model = new ViewModels.WorkOrderTypes.WorkOrderTypesListViewModel
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
			var model = new ViewModels.WorkOrderTypes.WorkOrderTypeItemViewModel
			{
				Item = Business.GetEntity<WorkOrderType>(id).Entity
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