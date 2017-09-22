using BudgetManager.Business.Base;
using BudgetManager.Common.Messages;
using BudgetManager.Data.Extensions;
using BudgetManager.Enums;
using BudgetManager.Extentions;
using BudgetManager.Models.Base;
using BudgetManager.Models.User;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;

namespace BudgetManager.Business.BankTransaction
{
	public class BankTransactionsManager : UserBusinessBase
	{
	    public BankTransactionsManager(User user) : base(user){}

		#region Fields
		private List<List<string>> _newBankTrasactionsRaw;
		#endregion

	    #region Private Properties

		private IQueryable<Models.User.BankTransaction> Query
		{
			get { return Db.BankTransactions.Where(i => i.UserId == User.Id && !i.IsDeleted); }
		}

		#endregion

		#region Public Properties

		public List<Models.User.BankTransaction> UserBankTransactions
		{
			get { return Query.ToList(); }
		}
		public List<Models.User.BankTransaction> AllBankTransactions
		{
			get { return Db.BankTransactions.ToList(); }
		}
		public List<BankTransactionGroup> AllBankTransactionGroups
		{
			get { return Db.BankTransactionGroups.ToList(); }
		}
		public List<User> AllUsers
		{
			get { return Db.Users.ToList(); }
		}
		public List<Models.Static.Bank> AllBanks
		{
			get { return Db.Banks.ToList(); }
		}

		private List<List<Models.User.BankTransaction>> NewBankTransactions { get; set; }

		private bool IsUserValid
		{
			get
			{
				if (User == null)
				{
					Result.Message = BankTransactionMessages.UserNotSet;
                    Result.Type = ResultType.Error;
					return false;
				}
                if (User.Id == Guid.Empty)
				{
					Result.Message = BankTransactionMessages.UserNew;
                    Result.Type = ResultType.Error;
					return false;
				}
				return true;
			}
		}
		#endregion

		#region Queries Get Methods

		public List<Models.User.BankTransaction> GetBankTransactionsIncludingAll(Expression<Func<Models.User.BankTransaction, bool>> filterExpression)
		{
			return Query
                .Include(i => i.BankAccount)
				.Include(i => i.User)
				.Include(i => i.BankTransactionGroup)
				.Where(filterExpression)
				.ToList();
		}
		public IQueryable<Models.User.BankTransaction> GetUserBankTransactions(Expression<Func<Models.User.BankTransaction, bool>> filterExpression = null)
		{
			var include = Query.Include(i => i.BankAccount).Include(i => i.BankTransactionGroup);
			return filterExpression != null
				? include.Where(filterExpression)
				: include;
		}

		#endregion

		#region Get
		public List<Models.Static.Bank> GetBanks(Expression<Func<Models.Static.Bank, bool>> filterExpression)
		{
			return Db.Banks.Where(filterExpression).ToList();
		}
		public List<BankAccount> GetBankAccounts(Guid userId)
		{
			return Db.BankAccounts.Where(i => i.UserId == userId).ToList();
		}
		public List<BankAccount> GetBankAccounts()
		{
			return Db.BankAccounts
                .Where(i => i.UserId == User.Id)
				.OrderBy(i => i.Name)
				.ToList();
		}
		public List<Models.Static.Bank> GetUsersBanks(Guid userid)
		{
			return Db.BankAccounts
				.Include(i => i.Bank)
				.Where(i => i.UserId == userid)
				.Select(i => i.Bank)
				.Distinct()
				.ToList();
		}
		public List<BankTransactionGroup> GetBankTransactionGroups()
		{
			return Db.BankTransactionGroups
				.OrderBy(i => i.Name)
				.ToList();
		}

	    public List<BankTransactionRule> GetRules()
		{
			var ids = Query.Select(i => i.BankAccountId).ToList();
			return Db.BankTransactionRules
				.Where(i => ids.Contains(i.BankAccountId))
				.ToList();
		}
		public List<BankTransactionRule> GetRulesIncluding()
		{
			var ids = Query.Select(i => i.BankAccountId).ToList();
			return Db.BankTransactionRules
				.Include(i => i.BankAccount)
				.Include(i => i.BankTransactionGroup)
				.Where(i => ids.Contains(i.BankAccountId))
				.OrderBy(i => i.Description)
				.ToList();
		}
		public BankTransactionRule GetRuleIncluding(Guid ruleId)
		{
			return Db.BankTransactionRules
				.Include(i => i.BankAccount)
                .Include(i => i.BankAccount.Bank)
                .Include(i => i.BankAccount.User)
				.Include(i => i.BankTransactionGroup)
				.FirstOrDefault(i => i.Id == ruleId);
		}
	    public BankTransactionGroup GetGroup(Guid groupId)
		{
			return Db.BankTransactionGroups.FirstOrDefault(i => i.Id == groupId);
		}
	    #endregion

		#region Update Group

		public bool UpdateGroup(List<Models.User.BankTransaction> bankTransactions)
		{
			var transactions = bankTransactions.Select(t => new { t.Id, TypeGroupId = t.GroupId });
			var ids = bankTransactions.Select(t => t.Id);
            var updated = Db.BankTransactions.Where(t => ids.Contains(t.Id)).Update(i => new Models.User.BankTransaction { GroupId = i.GroupId });
            return Db.SaveChanges() > 0;
		}

		#endregion

		#region Save
		public bool SaveNew()
		{
			try
			{
				var results = NewBankTransactions.Select(i => i.Select(GetValidationResult)).ToList();
				if (results.All(l => l.All(v => v.IsValid)))
				{
					if (NewBankTransactions != null && NewBankTransactions.Any())
					{
						NewBankTransactions.ForEach(list => list.ForEach(b =>
						{
                            Db.Entry(Db.BankTransactions.Attach(b)).State = EntityState.Added;
                        }));
						var saved = Db.SaveChanges() > 0;
						if (!saved)
						{
						    Result.Message = BankTransactionMessages.BankTransactionsNotSavedSomeOrAll;
                            Result.Type = ResultType.Error;
						}
                        else
						{
						    Result.Message = BankTransactionMessages.BankTransactionsSaved;
                            Result.Type = ResultType.Success;
						}
						return saved;
					}
                    Result.Message = BankTransactionMessages.BankTransactionsNotExistsAnyNew;
                    Result.Type = ResultType.Error;
					return false;
				}
				var errors = results
					.Where(i => i.Any(a => !a.IsValid))
					.SelectMany(l => l.Select(v => v.ValidationErrors))
					.SelectMany(i => i.Select(a => a.Value));
                Result.Message = ToMultilineString(errors);
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
		public bool Save(Models.User.BankTransaction banktransaction)
		{
			try
			{
				var validationResult = GetValidationResult(banktransaction);
				if (validationResult.IsValid)
				{
					Db.Entry(Db.BankTransactions.Attach(banktransaction)).State = EntityState.Added;
					Db.SaveChanges();
					return true;
				}
                Result.Message = ToMultilineString(validationResult);
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				return LogException(e);
			}
		}
		public bool Save(List<Models.User.BankTransaction> bankTransactions)
		{
			bankTransactions.ForEach(t => Db.Entry(Db.BankTransactions.Attach(t)).State = EntityState.Modified);
			return Db.SaveChanges() > 0;
		}

	    public bool SaveRule(BankTransactionRule rule)
		{
			var result = GetValidationResult(rule);
			if (!result.IsValid)
			{
                Result.Message = result.ValidationErrors.Select(i => i.Value).Concatenate();
                Result.Type = ResultType.Error;
				return false;
			}
			var saved = Db.BankTransactionRules.AddEntity(Db, rule);
			if (saved)
			{
                Result.Message = BankTransactionMessages.RuleSaved;
                Result.Type = ResultType.Success;
			}
			else
			{
                Result.Message = BankTransactionMessages.RuleNotSaved;
                Result.Type = ResultType.Error;
			}
			return saved;
		}
	    public bool Save(BankTransactionGroup bankTransactionGroup)
		{
			return Db.BankTransactionGroups.SaveEntity(Db, bankTransactionGroup);
		}
		public bool Save(BankTransactionRule bankTransactionGroup)
		{
			return Db.BankTransactionRules.SaveEntity(Db, bankTransactionGroup);
		}

	    #endregion

		#region Add Group
		public bool AddGroup(BankTransactionGroup group)
		{
			return Db.BankTransactionGroups.AddEntity(Db, @group);
		}
		#endregion

		#region Load
		public BankTransactionsManager Load(List<List<string>> list)
		{
			if (_newBankTrasactionsRaw == null) _newBankTrasactionsRaw = new List<List<string>>();
			_newBankTrasactionsRaw.AddRange(list);
			return this;
		}
		public BankTransactionsManager SetBankAccountOfNew(Guid bankAccountId)
		{
			if (NewBankTransactions != null && NewBankTransactions.Any())
				NewBankTransactions.ForEach(list => list.ForEach(b => b.BankAccountId = bankAccountId));
			return this;
		}
		#endregion

		#region Set
		public BankTransactionsManager SetGroupings()
		{
			// get rules
			var rules = Db.BankTransactionRules.ToList();
			// for each new bank transaction set grouping as per rule
			if (rules.Any())
				NewBankTransactions.ForEach(l => l.ForEach(t =>
				{
					var id = rules.Where(r => t.BankAccountId == r.BankAccountId 
										&& (r.IsCaseSensitive) 
											? t.Description.Contains(r.Text) 
											: t.Description.ToLower().Contains(r.Text.ToLower()))
									.Select(i => i.BankTransactionGroupId).FirstOrDefault();
					t.GroupId = id != Guid.Empty ? id : (Guid?) null;
				}));
			return this;
		}
		#endregion

		#region Delete
		/// <summary>
		/// Deletes all transactions.
		/// </summary>
		/// <returns></returns>
		public bool DeleteAllTransactions()
		{
            return Db.BankTransactions.Where(i => i.UserId == User.Id).Delete() > 0;
		}
		/// <summary>
		/// Removes the duplicate transactions.
		/// </summary>
		/// <returns></returns>
		public bool RemoveDuplicates()
		{
			if (!IsUserValid) return false;
			// get list if users bank transactions
			var transactions = Query.ToList();
			// get transactions to be removed
			var transactionsToKeep = GetTransactionsToKeep(transactions).Select(i => i.Id).ToList();
			var transactionsToDelete = GetTransactionsToDelete(transactions, transactionsToKeep).Select(i => i.Id).ToList();
			// delete transactions from Database
			if (transactionsToDelete.Any())
			{
                var deleted = Db.BankTransactions.Where(i => transactionsToDelete.Contains(i.Id)).Delete() > 0;
                //return if deleted or not;
				return deleted;
			}
			Result.Message = BankTransactionMessages.BankTransactionsRemoveDuplicatesNoDuplicates;
            Result.Type = ResultType.Information;
			return false;
		}
		#endregion

		#region Convertions
		public BankTransactionsManager Convert()
		{
			if (NewBankTransactions == null) NewBankTransactions = new List<List<Models.User.BankTransaction>>();
			NewBankTransactions.AddRange(_newBankTrasactionsRaw.Select(NewListOfBankTransactions));
			return this;
		}
		#endregion

		#region Private Helpers
		private List<Models.User.BankTransaction> NewListOfBankTransactions(List<string> list, int index)
		{
			const char csvSeparator = ',';
			return list.Select(row => NewBankTransaction(row.Split(csvSeparator).ToList(), index)).ToList();
		}
		private Models.User.BankTransaction NewBankTransaction(IReadOnlyList<string> row, int index)
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
		private static DateTime ConvertToDate(string dateString, string seperatorsString)
		{
			string convertToDate;
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
		private BankTransactionType GetTransactionType(string amount)
		{
			var amountInDouble = Double.Parse(amount, NumberStyles.Currency, CultureInfo.InvariantCulture);
			var isPositive = amountInDouble >= 0;
			return isPositive ? BankTransactionType.Credit : BankTransactionType.Debit;
		}
		private static List<Models.User.BankTransaction> GetTransactionsToDelete(List<Models.User.BankTransaction> transactions, IEnumerable<Guid> transactionsToKeep)
		{
			var transactionsToDelete = transactions
				.Where(i => !transactionsToKeep.Contains(i.Id))
				.ToList();
			return transactionsToDelete;
		}
		private static List<Models.User.BankTransaction> GetTransactionsToKeep(List<Models.User.BankTransaction> transactions)
		{
			var transactionsToKeep = transactions
				.GroupBy(i => new { i.TransactionDate, i.Description, i.Amount, i.Balance })
				.Select(i => i.FirstOrDefault())
				.ToList();
			return transactionsToKeep;
		}
		#endregion
	}
}
