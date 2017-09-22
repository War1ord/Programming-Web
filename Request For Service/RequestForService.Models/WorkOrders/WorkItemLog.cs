using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.WorkOrders
{
	public class WorkItemLog : Base.WorkItemCreatedByBase
	{
		[Required]
		public string Description { get; set; }

		public TimeSpan? TimeSpan { get; set; }

		public DateTime? DateStarted { get; set; }
		public DateTime? DateCompleted { get; set; }

		[UIHint("WorkItemStatus")]
		public DataTypes.Enums.WorkItemStatus Status { get; set; }

		[Required]
		[Display(Name = "Assigned To User")]
		public Guid AssignedToUserId { get; set; }

		[ForeignKey("AssignedToUserId")]
		public Users.User AssignedToUser { get; set; }

	}
}