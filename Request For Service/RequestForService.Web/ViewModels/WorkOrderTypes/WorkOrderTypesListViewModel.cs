using RequestForService.Models.WorkOrders;
using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.WorkOrderTypes
{
	public class WorkOrderTypesListViewModel : Base.ViewModelBase
	{
		public List<WorkOrderType> List { get; set; }
	}
}