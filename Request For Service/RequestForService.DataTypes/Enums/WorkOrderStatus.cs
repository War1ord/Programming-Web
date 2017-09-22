using System.ComponentModel.DataAnnotations;

namespace RequestForService.DataTypes.Enums
{
	public enum WorkOrderStatus
	{
		[Display(Name = "Created")]				Created			 = 10,
		[Display(Name = "Assigned")]			Assigned		 = 20,
		[Display(Name = "Viewed")]				Viewed			 = 30,
		[Display(Name = "On Hold")]				OnHold			 = 40,
		[Display(Name = "In Progress")]			InProgress		 = 50,
		[Display(Name = "Awaiting Feedback")]	AwaitingFeedback = 60,
		[Display(Name = "Completed")]			Completed		 = 70,
		[Display(Name = "Awaiting SignOff")]	AwaitingSignOff  = 80,
		[Display(Name = "Signed Off")]			SignedOff		 = 90,
		[Display(Name = "Cancelled")]			Cancelled		 = 100,
		[Display(Name = "Invoiced")]			Invoiced		 = 110,
		[Display(Name = "Closed")]				Closed			 = 120,
	}
}