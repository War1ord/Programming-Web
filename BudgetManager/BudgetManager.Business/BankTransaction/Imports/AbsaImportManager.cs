using BudgetManager.Enums;
using BudgetManager.Models.User;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace BudgetManager.Business.BankTransaction.Imports
{
	/// <summary>
	/// The Absa Bank Transaction Import Manager
	/// </summary>
	public class AbsaImportManager : ImportManagerBusinessBase
	{
	    /// <summary>
		/// Initializes a new instance of the <see cref="AbsaImportManager"/> class.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="extension">The extension.</param>
		/// <param name="user">The user.</param>
		public AbsaImportManager(string folderPath, string extension, User user) : base(folderPath, extension, user)
		{
		}

	    /// <summary>
		/// Saves the with SQL bulk copy.
		/// </summary>
		/// <returns></returns>
		public bool LoadDataIntoLists()
		{
			foreach (var csv in CsvFileReaderList)
				BankTransactions.AddRange(csv.CsvRawData.Skip(1).Select(NewBankTransaction).ToList());
			RemoveDuplicates();
			return true;
		}

	    #region Helpers
	    /// <summary>
		/// News the bank transaction.
		/// </summary>
		/// <param name="row">The row.</param>
		/// <param name="index">The index.</param>
		/// <returns></returns>
        private Models.User.BankTransaction NewBankTransaction(List<string> row, int index)
		{
            return new Models.User.BankTransaction
			{
				TransactionDate = ConvertToDate(row[0], @"\/-"),
				Description = row[1],
				Amount = Double.Parse(row[2], NumberStyles.Currency, CultureInfo.InvariantCulture),
				Balance = Double.Parse(row[3], NumberStyles.Currency, CultureInfo.InvariantCulture),
				TransactionSequence = index + 1,
				UserId = User.Id,
				BankTransactionType = GetTransactionType(row[2]),
				Created = DateTime.Now,
			};
		}

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="fileFullPath">The file full path.</param>
        /// <returns></returns>
        private static string GetFileName(string fileFullPath)
        {
            var lastIndexOf = fileFullPath.LastIndexOf('\\');
            var substring = fileFullPath.Substring(lastIndexOf + 1, fileFullPath.Length - lastIndexOf - 1);
            return substring;
        }

        /// <summary>
        /// Gets the file folder path.
        /// </summary>
        /// <param name="fileFullPath">The file full path.</param>
        /// <returns></returns>
        private static string GetFileFolderPath(string fileFullPath)
        {
            var lastIndexOf = fileFullPath.LastIndexOf('\\');
            var substring = fileFullPath.Substring(0, lastIndexOf);
            return substring;
        }

        /// <summary>
        /// Gets the type of the transaction.
        /// </summary>
        /// <param name="amount">The amount.</param>
        /// <returns></returns>
        private BankTransactionType GetTransactionType(string amount)
        {
            var amountInDouble = Double.Parse(amount, NumberStyles.Currency, CultureInfo.InvariantCulture);
            var isPositive = amountInDouble >= 0;
            return isPositive ? BankTransactionType.Credit : BankTransactionType.Debit;
        }

        /// <summary>
        /// Converts to dateString.
        /// </summary>
        /// <param name="dateString">The dateString.</param>
        /// <param name="seperatorsString">The seperators string.</param>
        /// <returns></returns>
        private static DateTime ConvertToDate(string dateString, string seperatorsString)
        {
            string convertToDate = String.Empty;
            var seperators = seperatorsString.ToCharArray();
            if (dateString.Length == 8 && dateString.IndexOfAny(seperators) < 0)
            {
                string year = dateString.Substring(0, 4);
                string month = dateString.Substring(4, 2);
                string day = dateString.Substring(6, 2);
                convertToDate = string.Format("{0}-{1}-{2}", year, month, day);
            }
            else
            {
                string[] dateStringSplit = dateString.Split(seperators);
                string year = dateStringSplit[0];
                string month = dateStringSplit[1];
                string day = dateStringSplit[2];
                convertToDate = string.Format("{0}-{1}-{2}", year, month, day);
            }
            return DateTime.Parse(convertToDate);
        } 
        #endregion

	    //#region Private Chained Methods

		///// <summary>
		///// Saves this instance.
		///// </summary>
		///// <returns></returns>
		//private AbsaImportManager Save()
		//{
		//	using (var db = new DataContext())
		//	{
		//		string data = string.Empty;
		//		foreach (var csv in CsvFileReaderList)
		//		{
		//			data = csv.Data.Concat();
		//			Import import = NewImport(data, csv, _user);
		//			AddImport(db, import);
		//			var bankTransactions = csv.CsvRawData.Skip(1)
		//				.Select((row, index) => NewBankTransaction(row, index, import.Id));
		//			foreach (var bankTransaction in bankTransactions)
		//			{
		//				AddBankTransaction(db, bankTransaction);
		//			}
		//		}
		//		db.SaveChanges();
		//	}
		//	return this;
		//}

		//#endregion
	}
}