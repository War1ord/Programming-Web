using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Models.ComplexTypes
{
	[ComplexType]
	public class Email
	{
		[Required, StringLength(maximumLength: 500)]
		public string Subject { get; set; }

		[Required]
		public string Body { get; set; }

		public bool IsHtml { get; set; }

	}
}