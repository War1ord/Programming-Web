using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.ComplexTypes
{
	[ComplexType]
	public class AddressInfo
	{
        [Display(Name = "Address Line")]
		public string AddressLine { get; set; }
        public string Suburb { get; set; }
		public string City { get; set; }
        [Display(Name = "Area Code"), DataType(DataType.PostalCode)]
		public string AreaCode { get; set; }
	}
}