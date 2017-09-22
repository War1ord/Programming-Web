using System.Web.WebPages;
using BudgetManager.Models.Base;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;
using BudgetManager.Web.Enums;
using BudgetManager.Web.ViewModels.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace BudgetManager.Web.Controllers
{
	public class BudgetController : BaseController
	{
		public ActionResult Index()
		{
			return RedirectToAction("Summary");
		}

		[HttpGet]
		public ActionResult Summary(string startdate = null, string enddate = null)
		{
			var model = new BudgetViewModel();

			#region Set Dates

			{
				DateTime startDateTime, endDateTime;
				model.StartDate = (!DateTime.TryParse(startdate, out startDateTime)) ? startDateTime : DateTime.Today.AddDays(-1);
				model.EndDate = (!DateTime.TryParse(enddate, out endDateTime)) ? endDateTime : DateTime.Today.AddMonths(1);
			}

			#endregion

			using (var manager = new Business.Budget.BudgetManager())
			{
				model.User = session.User;
				model.BudgetTemplateItems = manager.GetTemplatesIncludingItems(session.User.Id, model.StartDate, model.EndDate);
				model.BudgetTypeDates = manager.GetBudgetTypeDates(session.User.Id, model.StartDate, model.EndDate);
			}
			return View("Summary", model);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult Summary(BudgetViewModel model, BudgetActionEnum action)
		{
			switch (action)
			{
				case BudgetActionEnum.AddMonth:
				{
					return RedirectToAction("MonthAdd");
				}
				case BudgetActionEnum.AddTemplate:
				{
					return RedirectToAction("TemplateAdd");
				}
				case BudgetActionEnum.Refresh:
				{
					using (var manager = new Business.Budget.BudgetManager())
					{
						model.User = session.User;
						model.BudgetTemplateItems = manager.GetTemplatesIncludingItems(session.User.Id, model.StartDate, model.EndDate);
						model.BudgetTypeDates = manager.GetBudgetTypeDates(session.User.Id, model.StartDate, model.EndDate);
					}
					return View("Summary", model);
				}
			}
			return View("Summary", model);
		}

		[HttpGet]
		public ActionResult MonthAdd()
		{
			return View("Edit");
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult MonthAdd(BudgetAddViewModel model, BudgetActionEnum action)
		{
			switch (action)
			{
				case BudgetActionEnum.AddMonth:
				{
					if (ModelState.IsValid)
					{
						using (var manager = new Business.Budget.BudgetManager())
						{
							var today = DateTime.Today;
							var budgetTypeDate = new BudgetTypeDate()
							{
								StartDate = today,
								BudgetType = model.BudgetType,
								UserId = session.User.Id,
							};
							model.Result = manager.Save(ref budgetTypeDate, session.User.Id);
							if (model.Result.Type != ResultType.Success)
							{
								return View("Edit", model);
							}
							var items = manager.GetBudgetTemplates(session.User.Id).Select(template => new BudgetRowItem()
							{
								BudgetTemplateItemId = template.Id,
								UserId = session.User.Id,
								BudgetDate = today,
								BudgetTypeDateId = budgetTypeDate.Id,
							}).ToList();
							model.Result = manager.Save(items, session.User.Id);
							if (model.Result.Type == ResultType.Success)
							{
								return RedirectToAction("Summary");
							}
						}
					}
					break;
				}
				case BudgetActionEnum.Cancel:
				{
					return RedirectToAction("Index");
				}
			}
			return View("Edit", model);
		}

		[HttpGet]
		public ActionResult TemplateAdd()
		{
			var model = new BudgetTemplateItemViewModel();
			return View("Template/Edit", model);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult TemplateAdd(BudgetTemplateItemViewModel model, BudgetActionEnum action)
		{
			switch (action)
			{
				case BudgetActionEnum.AddTemplate:
				{
					if (ModelState.IsValid)
					{
						using (var manager = new Business.Budget.BudgetManager())
						{
							model.Result = manager.Save(model.BudgetTemplateItem, session.User.Id);
						}
						if (model.Result.Type == ResultType.Success) 
							return RedirectToAction("Summary");
					}
					break;
				}
				case BudgetActionEnum.Cancel:
				{
					return RedirectToAction("Summary");
				}
			}
			return View("Template/Edit", model);
		}

		[HttpGet]
		public ActionResult TemplateEdit(Guid userid)
		{
			var model = new BudgetTemplateItemViewModel();
			using (var manager = new Business.Budget.BudgetManager())
			{
				model.BudgetTemplateItem = manager.GetBudgetTemplateItemById(userid, session.User.Id);
			}
			return View("Template/Edit", model);
		}
		[HttpPost, ValidateAntiForgeryToken]
		public ActionResult TemplateEdit(BudgetTemplateItemViewModel model, BudgetActionEnum action)
		{
			switch (action)
			{
				case BudgetActionEnum.Save:
				{
					if (ModelState.IsValid)
					{
						using (var manager = new Business.Budget.BudgetManager())
						{
							model.Result = manager.Save(model.BudgetTemplateItem, session.User.Id);
						}
						switch (model.Result.Type)
						{
							case ResultType.Success:
							{
								return RedirectToAction("Summary");
							}
							default:
							{
								return View("Template/Edit", model);
							}
						}
					}
					break;
				}
				case BudgetActionEnum.Cancel:
				{
					return RedirectToAction("Summary");
				}
			}
			return View("Template/Edit", model);
		}

	}
}