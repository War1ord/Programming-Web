using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RequestForService.Web.ViewModels.Account
{
	public class LoginViewModel : Base.ViewModelBase
	{
		[Required, EmailAddress]
		public string Email { get; set; }
		[Required, PasswordPropertyText]
		public string Password { get; set; }
	}
}