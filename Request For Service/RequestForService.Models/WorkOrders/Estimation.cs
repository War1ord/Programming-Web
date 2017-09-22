using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class Estimation : Base.WorkOrderCreatedByBase
	{
		private List<WorkItem> workItems;

		[Required]
		public string Description { get; set; }
		[Required]
		public TimeSpan TimeSpan { get; set; }

		public List<WorkItem> WorkItems
		{
			get { return workItems ?? (workItems = new List<WorkItem>()); }
			set { workItems = value; }
		}
	}
}