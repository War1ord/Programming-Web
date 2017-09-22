using System.ComponentModel.DataAnnotations;

namespace RequestForService.DataTypes.Enums
{
	public enum WorkItemStatus
	{
		[Display(Name = "Not Started")]
		NotStarted = 10,
		[Display(Name = "In progress")]
		Inprogress = 20,
		[Display(Name = "Completed")]
		Completed = 30,
	}
}