using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using BudgetManager.Models;
using BudgetManager.Models.Base;

namespace BudgetManager.Business.Bank
{
	public class BankManager : Base.BusinessBase
	{
		public List<Models.Static.Bank> GetBanks()
		{
			return Db.Banks.ToList();
		}

		public Models.Static.Bank GetBank(Expression<Func<Models.Static.Bank, bool>> expression)
		{
			return Db.Banks.FirstOrDefault(expression);
		}

		public bool Save(Models.Static.Bank bank)
		{
			try
			{
                Db.Entry(Db.Banks.Attach(bank)).State = System.Data.Entity.EntityState.Added;
                Db.SaveChanges();
				return true;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
				Result.Message = "The data your trying to Save is not valid.";
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		public bool Update(Models.Static.Bank bank, EntityState modified)
		{
			try
			{
                Db.Entry(Db.Banks.Attach(bank)).State = modified;
				Db.SaveChanges();
				return true;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
                Result.Message = "The data your trying to Save is not valid.";
                Result.Type = ResultType.Error;
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		public bool Delete(Models.Static.Bank bank)
		{
			try
			{
                Db.Entry(bank).State = System.Data.Entity.EntityState.Deleted;
				Db.SaveChanges();
				return true;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
                Result.Message = "The data your trying to Save is not valid.";
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