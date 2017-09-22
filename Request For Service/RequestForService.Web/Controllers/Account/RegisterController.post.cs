using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Account
{
	public partial class RegisterController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Index(ViewModels.Registration.NewRegistrationViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.Create(model.Registration);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Login", "Account");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Validate(ViewModels.Account.ValidateViewModel model)
		{
			if (ModelState.IsValid)
			{
				model.Result = Business.Validate(model.Code, model.Email, model.Password);
				if (model.Result.IsSuccessful)
				{
					return RedirectToAction("Login", "Account");
				}
			}
			return View(model);
		}

	}
}