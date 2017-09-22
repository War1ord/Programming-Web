using BudgetManager.Business.Base;
using BudgetManager.Enums;
using BudgetManager.Extentions;
using BudgetManager.Models.User;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Business.BankTransaction
{
	/// <summary>
	/// The Class AbsaBankTransactionsDuplicateRemover, containing the functionality to remove duplicate bank transactions.
	/// </summary>
	public class AbsaBankTransactionsDuplicateRemover : UserBusinessBase
	{
		public AbsaBankTransactionsDuplicateRemover(User user) : base(user)
		{
			GetBankTransactions();
		}

	    #region Properties

		/// <summary>
		/// Gets or sets the bank transactions.
		/// </summary>
		/// <value>
		/// The bank transactions.
		/// </value>
		public List<Models.User.BankTransaction> BankTransactions { get; set; }

		/// <summary>
		/// Gets or sets the duplicates removed bank transactions.
		/// </summary>
		/// <value>
		/// The duplicates removed bank transactions.
		/// </value>
        public List<Models.User.BankTransaction> DuplicatesRemovedBankTransactions { get; set; }

		/// <summary>
		/// Gets the grouping key selector.
		/// </summary>
		/// <value>
		/// The grouping key selector.
		/// </value>
		private static Func<Models.User.BankTransaction, object> GroupingKeySelector
		{
			get { return g => new { g.TransactionDate, g.Description, g.Amount, g.Balance }; }
		}

		#endregion

	    #region Get

		/// <summary>
		/// Gets the bank transactions.
		/// </summary>
		public void GetBankTransactions()
		{
			if (IsUserValid(User))
			{
				BankTransactions = Db.BankTransactions.Where(t => t.UserId == User.Id).ToList();
			}
			else if (IsUserAdmin(User))
			{
				BankTransactions = Db.BankTransactions.ToList();
			}
		}

		#endregion

		#region Validations

		/// <summary>
		/// Checks if there Exists duplicates.
		/// </summary>
		/// <returns></returns>
		public bool ExistsDuplicates()
		{
			return BankTransactions.GroupBy(GroupingKeySelector).Any(i => i.Count() > 1);
		}

		/// <summary>
		/// Exists the duplicates still.
		/// </summary>
		/// <returns></returns>
		public bool ExistsDuplicatesStill()
		{
			return DuplicatesRemovedBankTransactions.GroupBy(GroupingKeySelector).Any(i => i.Count() > 1);
		}

		/// <summary>
		/// Determines whether [is user admin] [the specified user].
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>
		///   <c>true</c> if [is user admin] [the specified user]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsUserAdmin(User user)
		{
			return user.IsAdmin;
		}

		/// <summary>
		/// Determines whether [is user valid] [the specified user].
		/// </summary>
		/// <param name="user">The user.</param>
		/// <returns>
		///   <c>true</c> if [is user valid] [the specified user]; otherwise, <c>false</c>.
		/// </returns>
		private bool IsUserValid(User user)
		{
			return user != null && user.Id != Guid.Empty;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Removes the duplicates from a list of BankTransactions.
		/// </summary>
		/// <param name="bankTransactions">The bank transactions.</param>
		/// <returns>a distinct list of BankTransactions</returns>
        public List<Models.User.BankTransaction> RemoveDuplicates(List<Models.User.BankTransaction> bankTransactions)
		{
			return bankTransactions.GroupBy(GroupingKeySelector).Select(i => i.FirstOrDefault()).ToList();
		}

		/// <summary>
		/// Loads the removed duplicates.
		/// </summary>
		/// <returns></returns>
		public bool LoadRemovedDuplicates()
		{
			DuplicatesRemovedBankTransactions = RemoveDuplicates(BankTransactions);
			return DuplicatesRemovedBankTransactions != null;
		}

		/// <summary>
		/// Deletes the bank transactions.
		/// </summary>
		/// <returns></returns>
		public bool DeleteBankTransactions()
		{
			try
			{
				if (ExistsDuplicates())
				{
					//BankTransactions.ForEach(b => Db.Entry(Db.BankTransactions.Attach(b)).State = EntityState.Deleted);
					//var deleted = Db.SaveChanges();
					//return deleted > 0;
					var bankTransactionIds = BankTransactions.Select(i => i.Id);
                    return Db.BankTransactions.Where(i => bankTransactionIds.Contains(i.Id)).Delete() > 0;
                }
				return false;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}

		/// <summary>
		/// Inserts the duplicates removed bank transactions.
		/// </summary>
		/// <returns></returns>
		public bool InsertDuplicatesRemovedBankTransactions()
		{
			try
			{
				return
					InsertBulk(DuplicatesRemovedBankTransactions
                    .ToDataTable(Tables.BankTransactions.ToString(),
                    "User", "BankTransactionGroup", "BankAccount", "Bank", "Import"));
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}

		#endregion
	}
}