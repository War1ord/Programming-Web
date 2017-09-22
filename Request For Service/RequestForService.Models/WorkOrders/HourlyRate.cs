using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class HourlyRate : Base.BusinessEntityCreatedByBase
	{
		[Required]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }
		public string Description { get; set; }
	}
}