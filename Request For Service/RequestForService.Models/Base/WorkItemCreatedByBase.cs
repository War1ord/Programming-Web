using RequestForService.Models.WorkOrders;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class WorkItemCreatedByBase : CreatedByBase
	{
		[Required]
		[Display(Name = "Work Item")]
		public Guid WorkItemId { get; set; }

		[ForeignKey("WorkItemId")]
		public WorkItem WorkItem { get; set; }
	}
}