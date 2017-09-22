using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Account
{
	public partial class AccountController : Base.BaseController
	{
		private Business.Services.Users.AccountService _business;
		private Business.Services.Users.AccountService Business
		{
			get { return _business ?? (_business = new Business.Services.Users.AccountService(UserId)); }
		}

		[HttpGet, Filters.RequestForServiceAuthentication]
		public ActionResult Index()
		{
			var result = Business.GetUser(session.User.Id);
			var model = new ViewModels.Account.AccountViewModel
			{
				User = result.Entity,
			};
			return result.IsValidEntity ? View(model) : View("NotAuthorized");
		}

		[HttpGet]
		public ActionResult Login(string returnurl)
		{
			ViewBag.ReturnUrl = session.ReturnUrl = returnurl;
			return View();
		}

		[HttpGet]
		public ActionResult Logout()
		{
			ClearSession();
			return Redirect(session.ReturnUrl ?? Url.Action("Login"));
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (_business != null)
				{
					_business.Dispose();
					_business = null; 
				}
			}
			base.Dispose(disposing);
		}
	}
}