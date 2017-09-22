using RequestForService.Models.WorkOrders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class WorkOrderCreatedByBase : CreatedByBase
	{
		[Required]
		[Display(Name = "Work Order")]
		public Guid WorkOrderId { get; set; }

		[ForeignKey("WorkOrderId")]
		public WorkOrder WorkOrder { get; set; }
	}
}