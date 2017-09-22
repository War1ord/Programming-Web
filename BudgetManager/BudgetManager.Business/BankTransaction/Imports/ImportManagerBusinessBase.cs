using BudgetManager.Business.Base;
using BudgetManager.Common.FoldersAndFiles;
using BudgetManager.Data;
using BudgetManager.Extentions;
using BudgetManager.Models.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace BudgetManager.Business.BankTransaction.Imports
{
	/// <summary>
	/// The ImportManagerBase Class containing the ImportManager Base component
	/// </summary>
	public class ImportManagerBusinessBase : BusinessBase
	{
	    /// <summary>
		/// Initializes a new instance of the <see cref="ImportManagerBusinessBase"/> class.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="extension">The extension.</param>
		/// <param name="user">The user.</param>
		public ImportManagerBusinessBase(string folderPath, string extension, User user)
		{
			FolderPath = folderPath;
			Extension = extension;
			User = user;
			Errors = new List<Exception>();
			CsvFileReaderList = new List<CsvFileReader>();
            BankTransactions = new List<Models.User.BankTransaction>();
		}

		#region Fields

		/// <summary>
		/// The folder
		/// </summary>
		protected string FolderPath;
		/// <summary>
		/// The extension
		/// </summary>
		protected string Extension;
		/// <summary>
		/// The user.
		/// </summary>
		protected User User;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the folder manager.
		/// </summary>
		/// <value>
		/// The folder manager.
		/// </value>
		public FolderManager FolderManager { get; set; }
		/// <summary>
		/// Gets or sets the errors.
		/// </summary>
		/// <value>
		/// The errors.
		/// </value>
		public List<Exception> Errors { get; set; }
		/// <summary>
		/// Gets or sets the CSV file reader list.
		/// </summary>
		/// <value>
		/// The CSV file reader list.
		/// </value>
		public List<CsvFileReader> CsvFileReaderList { get; set; }
		/// <summary>
		/// Gets or sets the bank transactions.
		/// </summary>
		/// <value>
		/// The bank transactions.
		/// </value>
		public List<Models.User.BankTransaction> BankTransactions { get; set; }

		#endregion

	    #region Public Methods

		/// <summary>
		/// Imports the specified folder.
		/// </summary>
		/// <returns></returns>
		public bool ImportData()
		{
			if (User != null && User.Id != Guid.Empty)
			{
				Load(FolderPath, Extension);
				if (FolderManager.Error != null)
				{
					Errors.Add(FolderManager.Error);
					return false;
				}
				return true;
			}
			return false;
		}

		/// <summary>
		/// Saves the using SQL bulk copy manager.
		/// </summary>
		/// <returns></returns>
		public bool SaveUsingSqlBulkCopyManager()
		{
			bool saved = false;
			if (BankTransactions.Any())
			{
				Db.SaveChanges();
                saved = DataManagement.SqlBulkCopyToDatabase(
					ConvertToDataTable(BankTransactions), DataContext.ConnectionString);
			}
			return saved;
		}

		#endregion

		#region Data Helpers

		/// <summary>
		/// Adds the bank transaction.
		/// </summary>
		/// <param name="db">The db.</param>
		/// <param name="bankTransaction">The bank transaction.</param>
        protected void AddBankTransaction(DataContext db, Models.User.BankTransaction bankTransaction)
		{
			db.BankTransactions.Attach(bankTransaction);
            db.Entry(bankTransaction).State = System.Data.Entity.EntityState.Added;
		}

		/// <summary>
		/// Removes the duplicates.
		/// </summary>
		/// <param name="bankTransactions">The bank transactions.</param>
		public void RemoveDuplicates()
		{
			var duplicateRemover = new AbsaBankTransactionsDuplicateRemover(User);
			BankTransactions = duplicateRemover.RemoveDuplicates(BankTransactions);
		}

		#endregion

		#region Convert Helpers

		/// <summary>
		/// Converts to data table.
		/// </summary>
		/// <param name="bankTransactions">The bank transactions.</param>
		/// <returns></returns>
        protected DataTable ConvertToDataTable(List<Models.User.BankTransaction> bankTransactions)
		{
			var dataTable = new DataTable("BankTransactions");
			dataTable.Columns.AddRange(bankTransactions[0].ToDataColumnArray( /* Excluded Properties */ "Id", "User", "BankTransactionGroup", "BankTransactionGroup", "BankAccount"));
			bankTransactions.ForEach(b => { if (b == null) { return; }
				dataTable.Rows.Add(b.ToObjectArray( /* Excluded Properties */ "Id", "User", "BankTransactionGroup", "BankTransactionGroup", "BankAccount"));
			});
			return dataTable;
		}

		#endregion

		#region Chained Methods

		/// <summary>
		/// Loads the specified folder.
		/// </summary>
		/// <param name="folder">The folder.</param>
		/// <param name="extension">The extension.</param>
		/// <returns></returns>
		public ImportManagerBusinessBase Load(string folder, string extension)
		{
			FolderManager = new FolderManager(folder, extension);
			if (FolderManager.Files != null && FolderManager.Files.Count > 0)
			{
				foreach (var file in FolderManager.Files)
				{
					var csvFileReader = new CsvFileReader(file.FullName);
					CsvFileReaderList.Add(csvFileReader);
					if (csvFileReader.Error != null)
					{
						Errors.Add(csvFileReader.Error);
					}
				}
			}
			return this;
		}

		/// <summary>
		/// Loads the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns></returns>
		public ImportManagerBusinessBase Load(User user)
		{
			User = user;
			return this;
		}

		#endregion
	}
}