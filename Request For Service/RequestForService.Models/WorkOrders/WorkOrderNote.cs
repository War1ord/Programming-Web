using RequestForService.DataTypes.Enums;
using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrderNote : Base.WorkOrderCreatedByBase
	{
		[Required]
		public string Description { get; set; }
		[Required]
		public NoteType Type { get; set; }
	}
}