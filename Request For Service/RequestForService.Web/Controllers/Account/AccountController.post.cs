using System.Web.Mvc;

namespace RequestForService.Web.Controllers.Account
{
	public partial class AccountController
	{
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Login(ViewModels.Account.LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = Business.Authenticate(model.Email, model.Password);
				session.User = result.Entity;
				if (result.IsValidEntity)
				{
					return Redirect(session.ReturnUrl ?? Url.Action("Index", "WorkOrders"));
				}
				model.Result = result.ToResult();
			}
			return View(model);
		}
	}
}