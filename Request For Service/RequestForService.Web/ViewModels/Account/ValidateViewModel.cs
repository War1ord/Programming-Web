using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RequestForService.Web.ViewModels.Account
{
	public class ValidateViewModel : Base.ViewModelBase
	{
		[Required]
		public string Code { get; set; }
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required, PasswordPropertyText]
		public string Password { get; set; }
	}
}