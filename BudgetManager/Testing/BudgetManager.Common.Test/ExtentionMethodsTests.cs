using System;
using System.Linq.Expressions;
using BudgetManager.Enums;
using BudgetManager.Extentions;
using BudgetManager.Models;
using BudgetManager.Models.Static;
using BudgetManager.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Common.Test
{
	[TestClass]
	public class ExtentionMethodsTests
	{
		[TestMethod]
		public void ExtentionsGetObjectName()
		{
			{
				// test 1
				const string test1 = "123";
				string result = new {test1}.PropertyName();
				Assert.AreNotEqual("test", result);
			}
			{
				// test 2
				const string test2 = "test";
				string result2 = new {test2}.PropertyName();
				Assert.AreEqual("test2", result2);
			}
			{
				// test 3
				var foo = new {test3 = "test3"};
				string result3 = foo.PropertyName();
				Assert.AreEqual("test3", result3);
			}
		}

		[TestMethod]
		public void BankTransactionToObjectArray()
		{
			var bankTransaction = new BankTransaction
			{
				Description = "test",
				TransactionDate = DateTime.Today,
				Amount = 0,
				Balance = 0,
				GroupId = Guid.Empty,
				TransactionSequence = 0,
				UserId = Guid.Empty,
                BankAccountId = Guid.Empty,
				Created = DateTime.Now,
				BankTransactionType = BankTransactionType.Debit,
			};
			var objectArray = bankTransaction.ToObjectArray();
			var objectArray2 = bankTransaction.ToObjectArray(new[]
			{
			    // Excluded Properties
			    "Id",
			    "User",
			    "BankTransactionGroup",
			    "BankTransactionGroup",
			    "BankAccount",
			    "Bank",
			    "Import",
			});
			Assert.IsTrue(objectArray != null && objectArray.Length > 0);
			Assert.IsTrue(objectArray2 != null && objectArray2.Length > 0);
		}

		[TestMethod]
		public void BankTransactionToDataColumbArray()
		{
			var bankTransaction = new BankTransaction
			{
			    Description = "test",
			    TransactionDate = DateTime.Today,
			    Amount = 0,
			    Balance = 0,
                GroupId = Guid.Empty,
			    TransactionSequence = 0,
                UserId = Guid.Empty,
                BankAccountId = Guid.Empty,
			    Created = DateTime.Now,
			    BankTransactionType = BankTransactionType.Debit,
			};
			var dataColumnArray = bankTransaction.ToDataColumnArray();
			var dataColumnArray2 = bankTransaction.ToDataColumnArray(new[]
			    {
				    // Excluded Properties
				    "Id",
				    "User",
				    "BankTransactionGroup",
				    "BankTransactionGroup",
				    "BankAccount",
				    "Bank",
				    "Import",
			    });
			var dataColumnArray3 = bankTransaction.ToDataColumnArray( // Excluded Properties
				"Id",
				"User",
				"BankTransactionGroup",
				"BankTransactionGroup",
				"BankAccount",
				"Bank",
				"Import");
			Assert.IsTrue(dataColumnArray != null && dataColumnArray.Length > 0);
			Assert.IsTrue(dataColumnArray2 != null && dataColumnArray2.Length > 0);
			Assert.IsTrue(dataColumnArray3 != null && dataColumnArray3.Length > 0);
		}

		[TestMethod]
		public void MetaSetTest()
		{
			var bank = new Bank();
			const string testName = "Test Name";
			const string testDescription = "Test Description";
			bank.Set(
				Name => testName,
				Description => testDescription
				);
			Assert.IsTrue(!string.IsNullOrWhiteSpace(bank.Name) && !string.IsNullOrWhiteSpace(bank.Description) &&
			              bank.Name == testName && bank.Description == testDescription);
		}
	}
}