using System.ComponentModel.DataAnnotations;

namespace RequestForService.Web.ViewModels.Registration
{
	public class ValidationViewModel : Base.ViewModelBase
	{
		[EmailAddress]
		public string Email { get; set; }
		public string ValidationCode { get; set; }
	}
}