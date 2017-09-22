using BudgetManager.Business.BankTransaction.Imports;
using BudgetManager.Extentions;
using BudgetManager.Models.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BudgetManager.Business.Test
{
	[TestClass]
	public class ImportManagerTests
	{
		[TestMethod]
		public void ImportTestData()
		{
			#region Variables

			var folderPath = Environment.CurrentDirectory
			                 + @"\..\..\..\.." // because Sites project lays in Orchard's modules folder.
			                 + @"\Testing Data\DataImports\Absa Transation History";
			const string extension = "csv";

			#endregion

			#region Get User

			User user = new User();
			using (var u = new UserManager())
			{
				user = u.GetUser("Warlord");
			}

			#endregion

			#region Import Bank Transactions

			using (var importManager = new AbsaImportManager(folderPath, extension, user))
			{
				bool imported = importManager.ImportData();
				Assert.IsTrue(imported, "Test Data didn't import. ");
				bool loaded = importManager.LoadDataIntoLists();
				Assert.IsTrue(loaded, "Data did not load. ");
				bool saved = importManager.SaveUsingSqlBulkCopyManager();
				Assert.IsTrue(saved, "Data did not save. ");
				Assert.IsFalse(importManager.Errors != null && importManager.Errors.Count > 0,
				               "An error(s) have occurred, List Of Exception Messages. " +
				               importManager.Errors.Select(e => e.Message).Concatenate(", "));
				Assert.IsNotNull(importManager.CsvFileReaderList, "No File found or an exception have occurred.");
				Assert.IsTrue(importManager.CsvFileReaderList.Count > 0, "No File found or an exception have occurred.");
			}

			#endregion
		}

		[TestMethod]
		public void FindAndRemoveDuplicatesBeforeSaveTest()
		{
			#region Variables

			var folderPath = Environment.CurrentDirectory
							 + @"\..\..\..\.." // because Sites project lays in Orchard's modules folder.
							 + @"\Testing Data\DataImports\Absa Transation History";
			const string extension = "csv";
			User user = new User();

			#endregion

			#region Get User

			using (var u = new UserManager())
			{
				user = u.GetUser("Warlord");
			}

			#endregion

			using (var importManager = new AbsaImportManager(folderPath, extension, user))
			{
				bool imported = importManager.Load(user).ImportData();
				Assert.IsTrue(imported, "Data did not import.");
				bool loaded = importManager.LoadDataIntoLists();
				Assert.IsTrue(loaded, "Data did not loaded.");

				var anyDuplicates = importManager.BankTransactions
					.GroupBy(g => new { g.TransactionDate, g.Description, g.Amount, g.Balance, })
					.Any(i=>i.Count()>1);

				Assert.IsTrue(!anyDuplicates, "Duplicates found");
				Assert.IsFalse(importManager.Errors != null && importManager.Errors.Count > 0,
							   "An error(s) have occured, List Of Exception Messages. " +
							   importManager.Errors.Select(e => e.Message).Concatenate(", "));
				Assert.IsNotNull(importManager.CsvFileReaderList, "No File found or an exception have occured.");
				Assert.IsTrue(importManager.CsvFileReaderList.Count > 0, "No File found or an exception have occured.");
			}
		}

	}
}