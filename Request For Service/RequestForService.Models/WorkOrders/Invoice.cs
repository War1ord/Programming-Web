using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class Invoice : Base.WorkOrderCreatedByBase
	{
		[Required]
		public string InvoiceNumber { get; set; }
		[Required]
		public decimal Amount { get; set; }
	}
}