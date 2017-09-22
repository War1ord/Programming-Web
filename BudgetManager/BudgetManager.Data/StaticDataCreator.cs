using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using BudgetManager.Enums;
using BudgetManager.Models;
using BudgetManager.Models.ComplexTypes;
using BudgetManager.Models.Static;
using BudgetManager.Models.User;

namespace BudgetManager.Data
{
	public static class StaticDataCreator
	{
		public static void CreateStaticData()
		{
			using (var db = new DataContext())
			{
				CreateStandardBudgetTemplateItems(db);
				CreateStandardBudgetRowItems(db);
				CreateUser(db);
				CreateBanks(db);
				CreateBankAccounts(db);
			}
		}

		#region Methods

		/// <summary>
		/// Creates the standard budget template items.
		/// </summary>
		/// <param name="db">The db.</param>
		private static void CreateStandardBudgetTemplateItems(DataContext db)
		{
			try
			{
				if (!db.BudgetTemplateItems.Any())
				{
					NewBudgetTemplateItem().ForEach(item => AddBudgetTemplateItem(item, db));
					db.SaveChanges();
				}
				//Assert.IsTrue(db.BudgetTemplateItems.Any(), "There is no Budget Items, so not data was saved. Test failed!");
			}
			catch (Exception e)
			{
				LogException(e);
				//Assert.Fail(e.Message, e.Source, e.TargetSite, e.StackTrace, e.InnerException);
			}
		}

		/// <summary>
		/// Creates the standard budget row items.
		/// </summary>
		/// <param name="db">The db.</param>
		private static void CreateStandardBudgetRowItems(DataContext db)
		{
			try
			{
				if (!db.BudgetRowItems.Any())
				{
					var items = new List<BudgetRowItem>();
					foreach (var templateItem in db.BudgetTemplateItems)
					{
						items.Add(NewBudgetRowItem(templateItem, -1));
						items.Add(NewBudgetRowItem(templateItem, 0));
						items.Add(NewBudgetRowItem(templateItem, 1));
						items.Add(NewBudgetRowItem(templateItem, 2));
						items.ForEach(item => AddBudgetRowItem(item, db));
					}
					db.SaveChanges();
				}
				//Assert.IsTrue(db.BudgetRowItems.Any(), "There is no Budget Items, so not data was saved. Test failed!");
			}
			catch (Exception e)
			{
				LogException(e);
				//Assert.Fail(e.Message, e.Source, e.TargetSite, e.StackTrace, e.InnerException);
			}
		}

		/// <summary>
		/// Creates the user.
		/// </summary>
		/// <param name="db">The db.</param>
		private static void CreateUser(DataContext db)
		{
			var user = new User
			           {
				           Email = "kuperus.charles@gmail.com",
				           Password = "Enterlol123123_+",
				           IsAuthenticated = true,
				           IsValidated = true,
						   IsAdmin = true,
				           IsDeleted = false,
				           IsLocked = false,
			           };
			if (!db.Users.Any(u => u.Email.Contains(user.Email)))
			{
				db.Users.Attach(user);
				db.Entry(user).State = System.Data.Entity.EntityState.Added;
				db.SaveChanges();
			}
		}

		/// <summary>
		/// Creates the banks.
		/// </summary>
		/// <param name="db">The db.</param>
		private static void CreateBanks(DataContext db)
		{
			var list = new List<string> {"Absa Bank", "First National Bank", "Standard Bank", "Capitec Bank", "WesBank"};
			if (!db.Banks.Any(b => list.Any(l => b.Name.Contains(l))))
			{
				list.ForEach(item => AddBank(NewBank(item), db));
				db.SaveChanges();
			}
		}

		/// <summary>
		/// Creates the bank accounts.
		/// </summary>
		/// <param name="db">The db.</param>
		/// <exception cref="System.NotImplementedException"></exception>
		private static void CreateBankAccounts(DataContext db)
		{
			var list = new List<string> {"Cheque", "Credit", "Personal Loan", "Savings"};
			var absa = db.Banks.FirstOrDefault(i => i.Name.Contains("Absa"));
			var user = db.Users.FirstOrDefault(i => i.Email.Contains("kuperus.charles@gmail.com"));
			if (!db.BankAccounts.Include(i => i.User).Include(i=>i.Bank)
				     .Any(a => list.Any(l => a.Name.Contains(l))
				               && a.Bank.Name.Contains(absa.Name)
				               && a.User.Email.Contains(user.Email)))
			{
				list.ForEach(item => AddBankAccount(NewBankAccount(item, absa, user), db));
				db.SaveChanges();
			}
		}

		#endregion

		#region Helpers private

		/// <summary>
		/// Gets the budget amount.
		/// </summary>
		/// <param name="templateItem">The template item.</param>
		/// <returns></returns>
		private static decimal GetBudgetAmount(BudgetTemplateItem templateItem)
		{
			switch (templateItem.Name)
			{
				case "Rent": {
					return Convert.ToDecimal(3000);
				}
				case "Car Payments": {
					return Convert.ToDecimal(2100);
				}
				case "Car Insurance": {
					return Convert.ToDecimal(1100);
				}
				case "ABSA Loan": {
					return Convert.ToDecimal(1000);
				}
				case "Overdraft / Credit Card": {
					return Convert.ToDecimal(500);
				}
				case "Telephone": {
					return Convert.ToDecimal(600);
				}
				case "Petrol": {
					return Convert.ToDecimal(3000);
				}
				case "Tollgate": {
					return Convert.ToDecimal(320);
				}
				case "Internet": {
					return Convert.ToDecimal(200);
				}
				case "Snacks": {
					return Convert.ToDecimal(500);
				}
				case "Entertainment": {
					return Convert.ToDecimal(500);
				}
				case "Clothing": {
					return Convert.ToDecimal(400);
				}
				case "Bank Fees": {
					return Convert.ToDecimal(250);
				}
				case "Salary": {
					return Convert.ToDecimal(13500);
				}
				default:
					return Convert.ToDecimal(0);
			}
		}

		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="e">The e.</param>
		private static void LogException(Exception e)
		{
			//Business.ErrorManager.LogException(e);
			using (var db = new DataContext())
			{
				var error = new Models.Error(e);
				db.Errors.Add(error);
				db.SaveChanges();
			}
		}

		#region New

		/// <summary>
		/// News the standard list of budget template items.
		/// </summary>
		/// <returns></returns>
		private static List<BudgetTemplateItem> NewBudgetTemplateItem()
		{
			return new List<BudgetTemplateItem>
			       {
				       new BudgetTemplateItem {Name = "Rent", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "Car Payments", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "Car Insurance", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "ABSA Loan", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "Overdraft / Credit Card", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "Telephone", BudgetItemType = BudgetItemType.ExpenseFixed},
				       new BudgetTemplateItem {Name = "Petrol", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Tollgate", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Internet", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Snacks", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Entertainment", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Clothing", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Bank Fees", BudgetItemType = BudgetItemType.ExpenseVariable},
				       new BudgetTemplateItem {Name = "Salary", BudgetItemType = BudgetItemType.Income},
			       };
		}

		/// <summary>
		/// News the budget row item.
		/// </summary>
		/// <param name="templateItem">The template item.</param>
		/// <param name="budgetDateAddedMonths">The budget date added months.</param>
		/// <returns></returns>
		private static BudgetRowItem NewBudgetRowItem(BudgetTemplateItem templateItem, int budgetDateAddedMonths)
		{
			return new BudgetRowItem
			       {
				       Created = DateTime.Now,
				       BudgetTemplateItem = templateItem,
				       BudgetTemplateItemId = templateItem.Id,
				       BudgetDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1).AddMonths(budgetDateAddedMonths),
				       AmountBudget = GetBudgetAmount(templateItem),
				       AmountActual = 0,
			       };
		}

		/// <summary>
		/// New bank.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <returns></returns>
		private static Bank NewBank(string item)
		{
			return new Bank {Name = item, Description = item};
		}

		/// <summary>
		/// New bank account.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="absa">The absa.</param>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		private static BankAccount NewBankAccount(string item, Bank absa, User user)
		{
			return new BankAccount
			       {
				       Name = item,
				       Description = item,
				       BankId = absa.Id,
				       UserId = user.Id,
			       };
		}

		#endregion

		#region Add

		/// <summary>
		/// Adds the budget template item.
		/// </summary>
		/// <param name="budgetTemplateItem">The budget template item.</param>
		/// <param name="db">The db.</param>
		private static void AddBudgetTemplateItem(BudgetTemplateItem budgetTemplateItem, DataContext db)
		{
			db.BudgetTemplateItems.Attach(budgetTemplateItem);
			db.Entry(budgetTemplateItem).State = System.Data.Entity.EntityState.Added;
		}

		/// <summary>
		/// Adds the budget row item.
		/// </summary>
		/// <param name="budgetRowItem">The budget row item.</param>
		/// <param name="db">The db.</param>
		private static void AddBudgetRowItem(BudgetRowItem budgetRowItem, DataContext db)
		{
			db.BudgetRowItems.Attach(budgetRowItem);
			db.Entry(budgetRowItem).State = System.Data.Entity.EntityState.Added;
		}

		/// <summary>
		/// Adds the bank.
		/// </summary>
		/// <param name="bank">The bank.</param>
		/// <param name="db">The db.</param>
		private static void AddBank(Bank bank, DataContext db)
		{
			db.Banks.Attach(bank);
			db.Entry(bank).State = System.Data.Entity.EntityState.Added;
		}

		/// <summary>
		/// Adds the bank account.
		/// </summary>
		/// <param name="bankAccount">The bank account.</param>
		/// <param name="db">The db.</param>
		private static void AddBankAccount(BankAccount bankAccount, DataContext db)
		{
			db.BankAccounts.Attach(bankAccount);
			db.Entry(bankAccount).State = System.Data.Entity.EntityState.Added;
		}

		#endregion

		#endregion
	}
}