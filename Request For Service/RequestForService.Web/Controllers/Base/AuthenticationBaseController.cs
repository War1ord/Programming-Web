namespace RequestForService.Web.Controllers.Base
{
	[Filters.RequestForServiceAuthentication]
	public class AuthenticationBaseController : BaseController
	{
		protected void ReloadUser()
		{
			using (var business = new Business.Services.Users.AccountService(UserId))
			{
				session.User = business.GetUser(session.User.Id).Entity;
			}
		}
	}
}