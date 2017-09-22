using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Account
{
	public partial class RegisterController : Base.BaseController
	{
		private Business.Services.Users.RegistrationService _business;
		private Business.Services.Users.RegistrationService Business
		{
			get { return _business ?? (_business = new Business.Services.Users.RegistrationService(UserId)); }
		}

		[HttpGet]
		public ActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public ActionResult Validate(string code = null, string email = null)
		{
			var model = new ViewModels.Account.ValidateViewModel
			{
				Code = code,
				Email = email,
			};
			return View(model);
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