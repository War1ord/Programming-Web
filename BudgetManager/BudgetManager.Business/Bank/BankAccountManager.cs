using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using BudgetManager.Business.Base;
using BudgetManager.Common.Messages;
using BudgetManager.Models.Base;
using BudgetManager.Models.User;

namespace BudgetManager.Business.Bank
{
	public class BankAccountManager : BusinessBase
	{
		#region Fields

		private User _user;

		#endregion

		#region Constructors

		public BankAccountManager(User user)
		{
			_user = user;
		}

		#endregion

		#region Properties

		private IQueryable<BankAccount> Query
		{
			get { return Db.BankAccounts.Where(i=>i.UserId == _user.Id); }
		}

		#endregion

		public List<BankAccount> GetBankAccounts()
		{
			return Query.ToList();
		}

		public List<BankAccount> GetBankAccountsIncludeBank()
		{
			return Query.Include(b => b.Bank).ToList();
		}

		public List<BankAccount> GetBankAccounts(Expression<Func<BankAccount, bool>> expression)
		{
			return Query.Where(expression).ToList();
		}

		public BankAccount GetBankAccount(Expression<Func<BankAccount, bool>> expression)
		{
			return Query.FirstOrDefault(expression);
		}

		public List<User> GetUsers()
		{
			return Db.Users.ToList();
		}

		public List<Models.Static.Bank> GetBanks()
		{
			return Db.Banks.ToList();
		}

		public bool Save(BankAccount bankaccount)
		{
			try
			{
				Save(Db.BankAccounts.Attach(bankaccount));
				Db.SaveChanges();
				return true;
			}
			catch(DbEntityValidationException e)
			{
				Exception = e;
				Result.Message = Common.Messages.ValidationMessages.SaveNotValid;
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		public bool Update(BankAccount bankaccount)
		{
			try
			{
                Db.Entry(Db.BankAccounts.Attach(bankaccount)).State = System.Data.Entity.EntityState.Modified;
				Db.SaveChanges();
				return true;
			}
			catch(DbEntityValidationException e)
			{
				Exception = e;
                Result.Message = ValidationMessages.SaveNotValid;
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		public bool Delete(BankAccount bankaccount)
		{
			try
			{
				Db.Entry(bankaccount).State = System.Data.Entity.EntityState.Deleted;
				Db.SaveChanges();
				return true;
			}
			catch(DbEntityValidationException e)
			{
				Exception = e;
                Result.Message = ValidationMessages.SaveNotValid;
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

	}
}