using System;
using System.Collections.Generic;
using System.Data;
using BudgetManager.Enums;
using BudgetManager.Models;

namespace BudgetManager.Data.Test
{
	public class TestBase
	{
		/// <summary>
		/// News the standard list of budget template items.
		/// </summary>
		/// <returns></returns>
		protected List<BudgetTemplateItem> NewStandardListOfBudgetTemplateItems()
		{
			return new List<BudgetTemplateItem>
			       {
				       CreateStandardBudgetItem("Rent", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("Car Payments", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("Car Insurance", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("ABSA Loan", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("Overdraft / Credit Card", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("Telephone", BudgetItemType.Fixed),
				       CreateStandardBudgetItem("Petrol", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Tollgate", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Internet", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Snacks", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Entertainment", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Clothing", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Bank Fees", BudgetItemType.Variable),
				       CreateStandardBudgetItem("Salary", BudgetItemType.Income),
			       };
		}

		/// <summary>
		/// Creates the standard budget item.
		/// </summary>
		/// <param name="description">The description.</param>
		/// <param name="budgetItemType">Type of the budget item.</param>
		/// <returns></returns>
		protected BudgetTemplateItem CreateStandardBudgetItem(string description, BudgetItemType budgetItemType)
		{
			return new BudgetTemplateItem
			       {
				       Name = description,
				       BudgetItemType = budgetItemType,
			       };
		}

		/// <summary>
		/// Adds the budget template item.
		/// </summary>
		/// <param name="budgetTemplateItem">The budget template item.</param>
		/// <param name="db">The db.</param>
		protected void AddBudgetTemplateItem(BudgetTemplateItem budgetTemplateItem, Data.DataContext db)
		{
			db.BudgetTemplateItems.Attach(budgetTemplateItem);
			db.Entry(budgetTemplateItem).State = EntityState.Added;
		}

		/// <summary>
		/// Adds the budget row item.
		/// </summary>
		/// <param name="budgetRowItem">The budget row item.</param>
		/// <param name="db">The db.</param>
		protected void AddBudgetRowItem(BudgetRowItem budgetRowItem, Data.DataContext db)
		{
			db.BudgetRowItems.Attach(budgetRowItem);
			db.Entry(budgetRowItem).State = EntityState.Added;
		}

		/// <summary>
		/// News the budget row item.
		/// </summary>
		/// <param name="templateItem">The template item.</param>
		/// <param name="budgetDateAddedMonths">The budget date added months.</param>
		/// <returns></returns>
		protected BudgetRowItem NewBudgetRowItem(BudgetTemplateItem templateItem, int budgetDateAddedMonths)
		{
			return new BudgetRowItem
			       {
				       Id = 0,
				       Created = DateTime.Now,
				       BudgetTemplateItem = templateItem,
				       BudgetTemplateItemId = templateItem.Id,
				       BudgetDate = DateTime.Today.AddDays(-DateTime.Today.Day + 1)
					       .AddMonths(budgetDateAddedMonths),
				       AmountBudget = GetBudgetAmount(templateItem),
				       AmountActual = 0,
				       AmountVariance = 0,
			       };
		}

		/// <summary>
		/// Gets the budget amount.
		/// </summary>
		/// <param name="templateItem">The template item.</param>
		/// <returns></returns>
		protected decimal GetBudgetAmount(BudgetTemplateItem templateItem)
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
		protected void LogException(Exception e)
		{
			Business.ErrorManager.LogException(e);
		}
	}
}