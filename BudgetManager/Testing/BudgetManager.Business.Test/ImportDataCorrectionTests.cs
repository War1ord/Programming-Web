using System;
using BudgetManager.Business.Bank;
using BudgetManager.Business.BankTransaction;
using BudgetManager.Business.Error;
using BudgetManager.Models;
using BudgetManager.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BudgetManager.Business.Test
{
	[TestClass]
	public class ImportDataCorrection
	{
		public User User { get; private set; }

		public ImportDataCorrection()
		{
			#region Get User

			using (var manager = new UserManager())
			{
				User = manager.GetUser("Warlord");
			}

			#endregion
		}

		[TestMethod]
		public void BankTrasactionsDuplicateRemoverLoadData()
		{
			try
			{
				var importDuplicateRemover = new AbsaBankTransactionsDuplicateRemover(User);
				Assert.IsTrue(importDuplicateRemover.BankTransactions.Count > 0, "No Data in BankTransactions");
			}
			catch (Exception e)
			{
				ErrorManager.LogException(e);
				Assert.Fail(e.Message);
			}
		}

		[TestMethod]
		public void FindDuplicateData()
		{
			var duplicateRemover = new AbsaBankTransactionsDuplicateRemover(User);
			Assert.IsTrue(!duplicateRemover.ExistsDuplicates(), "There is still Existing Duplicates");
		}

		[TestMethod]
		public void RemoveDuplicateBankTransactionsAndReplaceTest()
		{
			using (var duplicateRemover = new AbsaBankTransactionsDuplicateRemover(User))
			{
				var loaded = duplicateRemover.LoadRemovedDuplicates();
				Assert.IsTrue(loaded, "DuplicateRemover did not load.");
				Assert.IsTrue(duplicateRemover.ExistsDuplicates(), "DuplicateRemover did not load.");
				var deleted = duplicateRemover.DeleteBankTransactions();
				Assert.IsTrue(deleted, "DuplicateRemover did not delete.");
				var inserted = duplicateRemover.InsertDuplicatesRemovedBankTransactions();
				Assert.IsTrue(inserted, "DuplicateRemover did not inserted.");
				duplicateRemover.GetBankTransactions();
				Assert.IsTrue(duplicateRemover.ExistsDuplicatesStill(), "There is still Duplicates.");
			}
		}
	}
}