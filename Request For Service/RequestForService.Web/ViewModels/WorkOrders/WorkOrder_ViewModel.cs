using System.Collections.Generic;
using RequestForService.Business.Models;

namespace RequestForService.Web.ViewModels.WorkOrders
{
	public class WorkOrder_ViewModel : Base.ViewModelBase
	{
		public WorkOrder_ViewModel()
		{
            WorkOrder = new RequestForService.Models.WorkOrders.WorkOrder();
			WorkOrderTypes = new List<EntityDisplay>();
			BusinessEntities = new List<EntityDisplay>();
		}

        public RequestForService.Models.WorkOrders.WorkOrder WorkOrder { get; set; }
		public List<EntityDisplay> WorkOrderTypes { get; set; }
		public List<EntityDisplay> BusinessEntities { get; set; }
	}
}