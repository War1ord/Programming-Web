using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.WorkOrders
{
	public class Category : Base.BusinessEntityCreatedByBase
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }
	}
}