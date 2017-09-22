using RequestForService.DataTypes.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.WorkOrders
{
	public class WorkItem : Base.WorkOrderCreatedByBase
	{
		private List<WorkItemLog> logs;

		[Required]
		public string Description { get; set; }

		public TimeSpan? TimeSpan { get; set; }

		public DateTime? DateStarted { get; set; }
		public DateTime? DateCompleted { get; set; }
		public DateTime? DateSignedOff { get; set; }

		public bool IsSignedOff { get; set; }

		[UIHint("WorkItemStatus")]
		public WorkItemStatus Status { get; set; }

		[Required]
		[Display(Name = "Assigned To User")]
		public Guid AssignedToUserId { get; set; }
		[Display(Name = "Signed Off User")]
		public Guid? SignedOffUserId { get; set; }
		[Display(Name = "Estimation")]
		public Guid? EstimationId { get; set; }

		[ForeignKey("AssignedToUserId")]
		public Users.User AssignedToUser { get; set; }
		[ForeignKey("SignedOffUserId")]
		public Users.User SignedOffUser { get; set; }
		[ForeignKey("EstimationId")]
		public Estimation Estimation { get; set; }

		public List<WorkItemLog> Logs
		{
			get { return logs ?? (logs = new List<WorkItemLog>()); }
			set { logs = value; }
		}

	}
}