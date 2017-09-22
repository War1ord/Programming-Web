namespace RequestForService.Web.Models
{
	public class Session
	{
		public string ReturnUrl { get; set; }
		public RequestForService.Models.Users.User User { get; set; }
	}
}