using RequestForService.Business.Models;
using RequestForService.Business.Services.WorkOrders;
using RequestForService.Models.BusinessEntities;
using RequestForService.Models.WorkOrders;
using System;
using System.Web.Mvc;
using RequestForService.Web.ViewModels.WorkOrders;

namespace RequestForService.Web.Controllers.WorkOrders
{
	public partial class WorkOrdersController : Base.AuthenticationBaseController
	{
		private WorkOrderService _business;
		private WorkOrderService Service
		{
			get { return _business ?? (_business = new WorkOrderService(UserId)); }
		}

		public ActionResult Index(WorkOrder_List_ViewModel model)
		{
			model.List = Service.GetWorkOrderSummaryList(model.Parameters).Entity;
			model.BusinessEntityList = Service.GetBusinessEntitiesListByParent(null).Entity;
			model.UsersList = Service.GetUsersList(model.SelectedBusinessEntityId).Entity;
			return View(model);
		}

		public ActionResult Create()
		{
			var model = new WorkOrder_ViewModel
			{
                WorkOrderTypes = Service.GetEntityDisplayList<WorkOrderType, string>(i => new EntityDisplay { Id = i.Id, Value = i.Name }, i => i.Name).Entity,
                BusinessEntities = Service.GetEntityDisplayList<BusinessEntity, string>(i => new EntityDisplay { Id = i.Id, Value = i.Name }, i => i.Name).Entity,
			};
			return View(model);
		}

		public ActionResult Details(Guid id)
		{
            var model = new WorkOrder_Details_ViewModel
            {
                WorkOrder = Service.GetWorkOrderByIdIncludingProperties(id),
            };
            return View(model);
		}

        public ActionResult Edit(Guid id)
        {
            throw new NotImplementedException();
        }

	    public ActionResult Print()
	    {
	        throw new NotImplementedException();
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
