using AutoMapper;
using System;
using BudgetManager.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BudgetManager.Extentions;

namespace BudgetManager.Models.Test
{
	[TestClass]
	public class AutoMapper
	{
		[TestMethod]
		public void MapModelToUserDefinedClass()
		{
			var budgetRowItem = new BudgetRowItem
			{
                Id = Guid.Empty,
				Created = DateTime.Now,
				AmountBudget = 10,
				AmountActual = 5,
				BudgetDate = DateTime.Now
			};
			Mapper.CreateMap<BudgetRowItem, BudgetRowItemTest>().IgnoreAllUnmapped();
			Mapper.CreateMap<BudgetRowItemTest, BudgetRowItem>().IgnoreAllUnmapped();
			BudgetRowItemTest budgetRowItemTest = Mapper.Map<BudgetRowItem, BudgetRowItemTest>(budgetRowItem);
			BudgetRowItem rowItem = Mapper.Map<BudgetRowItemTest, BudgetRowItem>(budgetRowItemTest);
			Assert.IsNotNull(rowItem);
			Assert.IsNotNull(budgetRowItemTest);
			Mapper.AssertConfigurationIsValid();
		}

		#region Classes

		private class BudgetRowItemTest
		{
			public DateTime BudgetDate { get; set; }

			public decimal AmountBudget { get; set; }
		}

		#endregion
	}
}