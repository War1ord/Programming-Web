using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrderHistory : Base.WorkOrderCreatedByBase
	{
		[Required]
		public DataTypes.Enums.Change Change { get; set; }
		[Required]
		public string PropertyName { get; set; }
		[Required]
		public string OldValuesDescription { get; set; }
		[Required]
		public string NewValuesDescription { get; set; }
	}
}