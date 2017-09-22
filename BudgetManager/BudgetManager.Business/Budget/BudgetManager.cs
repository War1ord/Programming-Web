using BudgetManager.Business.Base;
using BudgetManager.Data;
using BudgetManager.Data.Extensions;
using BudgetManager.Enums;
using BudgetManager.Extentions;
using BudgetManager.Models.Base;
using BudgetManager.Models.User;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using NHibernate.Transform;

namespace BudgetManager.Business.Budget
{
	/// <summary>
	/// The Budget Manager Business Class. Encapsulating all the business logic.
	/// </summary>
	public class BudgetManager : BusinessBase
	{
		/// <summary>
		/// Gets the items including template item.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public List<BudgetRowItem> GetItemsIncludingTemplateItem(Guid userId)
		{
			try
			{
				return Db.BudgetRowItems
					.Where(item => item.UserId == userId)
					.Include(i => i.BudgetTemplateItem)
					.ToList();
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		/// <summary>
		/// Gets the items including template item.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <param name="startDateTime"></param>
		/// <param name="endDateTime"></param>
		/// <returns></returns>
		public List<BudgetTemplateItem> GetTemplatesIncludingItems(Guid userId, DateTime startDateTime, DateTime endDateTime)
		{
			try
			{
				//return Db.BudgetTemplateItems
				//    .Include(i => i.BudgetRowItems)
				//    .Include(i => i.BudgetRowItems.Select(r => r.BudgetTypeDate))
				//    .Where(item => item.UserId == userId 
				//        && item.BudgetRowItems.Any(rowItem => rowItem.BudgetDate >= startDateTime && rowItem.BudgetDate <= endDateTime))
				//    .ToList();

				/* http://stackoverflow.com/questions/4720573/linq-to-entities-how-to-filter-on-child-entities */
				/* TODO: test if code works */
				return Db.BudgetTemplateItems
					.Include(i => i.BudgetRowItems)
					.Include(i => i.BudgetRowItems.Select(r => r.BudgetTypeDate))
					.Where(template => template.UserId == userId)
					.Select(template => new
					{
						Template = template,
						RowItems = template.BudgetRowItems
							.Where(rowItem =>
								rowItem.BudgetDate >= startDateTime
								&& rowItem.BudgetDate <= endDateTime)
					})
					.Select(i => i.Template)
					.ToList();
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		/// <summary>
		/// Gets the budget row items including templates by budget date.
		/// </summary>
		/// <param name="budgetDate">For date.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public IEnumerable<BudgetRowItem> GetItemsByDate(DateTime budgetDate, Guid userId)
		{
			try
			{
				return Db.BudgetRowItems
					.Where(i => i.BudgetDate == budgetDate && i.UserId == userId)
					.Include(i => i.BudgetTemplateItem)
					.ToList();
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		/// <summary>
		/// Budgets the template items.
		/// </summary>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public IEnumerable<BudgetTemplateItem> GetBudgetTemplates(Guid userId)
		{
			try
			{
				return Db.BudgetTemplateItems
					.Where(item => item.UserId == userId)
					.ToList();
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		public BudgetTemplateItem GetBudgetTemplateItemById(Guid templateId, Guid userId)
		{
			try
			{
				return Db.BudgetTemplateItems
					.Where(item => item.Id == templateId && item.UserId == userId)
					.Include(item => item.BudgetRowItems)
					.FirstOrDefault();
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		public BudgetTypeDate GetBudgetTypeDateById(Guid typeId, Guid userId)
		{
			try
			{
				return Db.BudgetTypeDates.FirstOrDefault(item => item.Id == typeId && item.UserId == userId);
			}
			catch (Exception e)
			{
				LogException(e);
				return null;
			}
		}

		/// <summary>
		/// Inserts the bulk.
		/// </summary>
		/// <param name="budgetRowItems">The budget row items.</param>
		/// <param name="userId">The user identifier.</param>
		/// <returns></returns>
		public Result InsertBulk(List<BudgetRowItem> budgetRowItems, Guid userId)
		{
			try
			{
				budgetRowItems.ForEach(item => item.UserId = userId);
				InsertBulk(budgetRowItems.ToDataTable(Tables.BudgetRowItems.ToString(), "BudgetTemplateItem"));
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result HardDeleteBudgetRowItems(List<Guid> itemIds)
		{
			try
			{
				Db.BudgetRowItems.Where(item => itemIds.Contains(item.Id)).Delete();
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result SoftDeleteBudgetRowItems(List<Guid> itemIds)
		{
			try
			{
				Db.BudgetRowItems.Where(item => itemIds.Contains(item.Id)).Update(item => new BudgetRowItem() { IsDeleted = true });
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result Save(List<BudgetRowItem> list, Guid userId)
		{
			try
			{
				list.ForEach(template =>
				{
					template.UserId = userId;
					Db.AttachChanges(template);
				});
				Db.SaveChanges();
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result Save(List<BudgetTemplateItem> list, Guid userId)
		{
			try
			{
				list.ForEach(item =>
				{
					item.UserId = userId;
					Db.AttachChanges(item);
				});
				Db.SaveChanges();
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result Save(BudgetTemplateItem template, Guid userId)
		{
			try
			{
				template.UserId = userId;
				Db.AttachChanges(template);
				Db.SaveChanges();
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result Save(BudgetRowItem item, Guid userId)
		{
			try
			{
				item.UserId = userId;
				Db.AttachChanges(item);
				Db.SaveChanges();
				return new Result("Saved", ResultType.Success);
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public Result Save(ref BudgetTypeDate item, Guid userId)
		{
			try
			{
				item.UserId = userId;
				var date = item.StartDate.Date;
				var typeDate = Db.BudgetTypeDates.FirstOrDefault(i => i.StartDate <= date || i.StartDate >= date);//type already created
				if (typeDate == null)
				{
					Db.AttachChanges(item);
					Db.SaveChanges();
					return new Result("Saved", ResultType.Success);
				}
				else
				{
					//TODO:CharlesK:Create new budgettypedate and set to item(BudgetTypeDate)
					//return new Result("This date already exists.", ResultType.Warning);
					item = typeDate;
					return new Result(string.Empty, ResultType.Success);
				}
			}
			catch (Exception e)
			{
				LogException(e);
				return new Result("Unexpected error.", ResultType.Error);
			}
		}

		public List<BudgetTypeDate> GetBudgetTypeDates(Guid userId, DateTime startDate, DateTime endDate)
		{
			var list = new List<BudgetTypeDate>();
			try
			{
				list = Db.BudgetTypeDates.Where(i => i.UserId.Equals(userId)
							&& i.StartDate >= startDate
							&& i.StartDate <= endDate)
						.ToList();
				list.ForEach(i => i.Result.Set(string.Empty, ResultType.None));
			}
			catch (Exception exception)
			{
				list = new List<BudgetTypeDate>()
				{
					new BudgetTypeDate()
					{
						Result = new Result(Error.ErrorManager.LogException(exception), ResultType.Error)
					}
				};
			}
			return list;
		}
	}
}