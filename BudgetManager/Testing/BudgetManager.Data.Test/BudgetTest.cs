using BudgetManager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Data.Test
{
	[TestClass]
	public class BudgetTest : TestBase
	{
		[TestMethod]
		public void BudgetGroupingTest()
		{
			using (var db = new Data.DataContext())
			{
				List<BudgetTemplateItem> budgetItems = db.BudgetTemplateItems.ToList();
				var budgetDbQuery = db.BudgetRowItems;
				var budgetDetails = budgetDbQuery
					.GroupBy(b => b.BudgetTemplateItem)
					.ToList();
				Assert.IsTrue(budgetDetails.Any());
			}
		}
	}
}