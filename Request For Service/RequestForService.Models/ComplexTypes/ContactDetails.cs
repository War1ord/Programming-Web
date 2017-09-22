using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.ComplexTypes
{
	[ComplexType]
	public class ContactDetails
	{
		[Phone]
		public string Office { get; set; }
		[Phone]
		public string Mobile { get; set; }
	}
}