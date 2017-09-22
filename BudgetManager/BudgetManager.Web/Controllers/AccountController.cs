using BudgetManager.Business;
using BudgetManager.Common.Messages;
using BudgetManager.Extentions;
using BudgetManager.Models.Base;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;
using BudgetManager.Web.ViewModels;
using System;
using System.Globalization;
using System.Web.Mvc;

namespace BudgetManager.Web.Controllers
{
	public class AccountController : BaseController
	{
		public ActionResult Index()
		{
			try
			{
				using (var userManager = new UserManager())
				{
					return View(userManager.GetUsers());
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return View();
			}
		}

		public ActionResult Register()
		{
			return View();
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Register(RegistrationViewModel model)
		{
			try
			{
				model.User.Password = model.Password;
				string passwordHash = model.User.Password.ToPasswordHash();
				var value = new ValueProviderResult(passwordHash, passwordHash, CultureInfo.InvariantCulture);
				const string propertyName = "User.PasswordHash";
				ModelState[propertyName].Errors.Clear();
				ModelState.SetModelValue(propertyName, value);
				if (ModelState.IsValid)
				{
					using (var userManager = new UserManager())
					{
						bool added = userManager.SetUser(model.User).Add();
						return RedirectToAction("Index");
					}
				}
			}
			catch (Exception e)
			{
				LogException(e);
			}
			return View(model);
		}

		public ActionResult Validate()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Validate(ValidationViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					using (var userManager = new UserManager(model.Email))
					{
						bool loaded = userManager.Load();
						if (loaded && userManager.IsUserAndPasswordValid(model.Password)
							&& userManager.IsValidOneTimePin(model.OneTimePin))
						{
							if (userManager.UpdateToValidated() && userManager.ClearOneTimePin() &&
								userManager.UpdateToAuthorized())
							{
								session.User = userManager.GetUser();
								return RedirectToReturnUrl();
							}
							model.Result.Message = CommonMessages.UnexpectedError;
							model.Result.Type = ResultType.Error;
						}
						else
						{
							model.Result.Message =
								"Email, password or the One Time Pin entered is not valid or user does not exist.";
							model.Result.Type = ResultType.Error;
						}
					}
				}
			}
			catch (Exception e)
			{
				LogException(e);
			}
			return View(model);
		}

		public ActionResult Login()
		{
			return View();
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Login(LoginViewModel model)
		{
			try
			{
				if (ModelState.IsValid)
				{
					using (var userManager = new UserManager(model.Email))
					{
						bool loaded = userManager.Load();
						if (loaded && userManager.IsUserAndPasswordValid(model.Password))
						{
							if (userManager.IsUserValidated())
							{
								session.User = userManager.GetUser();
								return RedirectToReturnUrl();
							}
							return RedirectToAction("Validate", "Account");
						}
						model.Result.Message = "Email or password entered is not valid or user does not exist.";
						model.Result.Type = ResultType.Error;
					}
				}
			}
			catch (Exception e)
			{
				LogException(e);
			}
			return View(model);
		}

		public ActionResult Logout()
		{
			session.User = null;
			return RedirectToAction("Login");
		}

		public ActionResult Edit(Guid? id = null)
		{
			try
			{
				using (var userManager = new UserManager())
				{
					if (id.HasValue)
					{
						User user = userManager.GetUser(id.Value);
						if (user == null)
						{
							return HttpNotFound();
						}
						return View(user);
					}
					userManager.Load(session.User.Email);
					return View(userManager.GetUser());
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return View();
			}
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Edit(User user)
		{
			try
			{
				if (ModelState.IsValid)
				{
					using (var userManager = new UserManager(user))
					{
						userManager.Update();
						return RedirectToAction("Index");
					}
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return View();
			}
			return View(user);
		}

		public ActionResult Delete(Guid? id = null)
		{
			try
			{
				if (id.HasValue && id.Value != Guid.Empty)
				{
					using (var userManager = new UserManager())
					{
						User user = userManager.GetUser(id.Value);
						if (user == null)
						{
							return HttpNotFound();
						}
						return View(user);
					}
				}
				return HttpNotFound();
			}
			catch (Exception e)
			{
				LogException(e);
				return View();
			}
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Delete(User user)
		{
			try
			{
				using (var userManager = new UserManager())
				{
					bool deleted = userManager.Delete(user);
					return RedirectToAction("Index");
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return View();
			}
		}
	}
}