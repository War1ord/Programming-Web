using System.ComponentModel.DataAnnotations;
using RequestForService.Models.Base;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrderComment : WorkOrderCreatedByBase
	{
		[Required]
		public string Description { get; set; }
	}
}