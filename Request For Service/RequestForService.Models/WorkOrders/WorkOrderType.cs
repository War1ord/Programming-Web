using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrderType : Base.BusinessEntityCreatedByBase
	{
		public string Name { get; set; }
		public string Description { get; set; }
	}
}