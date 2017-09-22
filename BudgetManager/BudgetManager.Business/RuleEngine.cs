using BudgetManager.Business.Base;
using BudgetManager.Business.Error;
using BudgetManager.Data.Extensions;
using BudgetManager.Enums;
using BudgetManager.Extentions;
using BudgetManager.Models.User;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace BudgetManager.Business
{
	/// <summary>
	/// The RuleEngine Class that contains the functionaasdlity
	/// </summary>
	public class RuleEngine : BusinessBase
	{
		#region Properties

		/// <summary>
		/// Gets the bank transaction rules.
		/// </summary>
		/// <value>
		/// The bank transaction rules.
		/// </value>
		public List<BankTransactionRule> CurrentRules { get; set; }
		/// <summary>
		/// Gets or sets a new set of rules.
		/// </summary>
		/// <value>
		/// The new rule build.
		/// </value>
		public List<BankTransactionRule> NewRuleBuild { get; set; }

		#endregion

		#region Queries

		/// <summary>
		/// Gets the rule Query.
		/// </summary>
		/// <value>
		/// The rules.
		/// </value>
		private IQueryable<BankTransactionRule> Rules
		{
			get { return Db.BankTransactionRules; }
		}
		/// <summary>
		/// Gets the rule Query with includings.
		/// </summary>
		/// <value>
		/// The rules including.
		/// </value>
		private IQueryable<BankTransactionRule> RulesIncluding
		{
            get { return Db.BankTransactionRules.Include(i => i.BankAccount).Include(i => i.BankAccount.User); }
		}

		#endregion

		#region Load

		/// <summary>
		/// Loads the rules of userid.
		/// </summary>
		/// <param name="userid">The userid.</param>
		/// <returns>this</returns>
		public RuleEngine Load(Guid userid)
		{
			CurrentRules = RulesIncluding
				.Where(i => i.BankAccount.User.Id.Equals(userid))
				.ToList();
			return this;
		}

		/// <summary>
		/// Loads the specified group.
		/// </summary>
		/// <param name="group">The group.</param>
		/// <returns></returns>
		public RuleEngine LoadGroupToBuild(BankTransactionGroup group)
		{
            Db.Entry(Db.BankTransactionGroups.Attach(group)).State = group.Id != Guid.Empty ? System.Data.Entity.EntityState.Modified : System.Data.Entity.EntityState.Added;
			NewRuleBuild.ForEach(b =>
			{
			    b.BankTransactionGroup = group; 
				b.BankTransactionGroupId = group.Id;
			});
			return this;
		}

		/// <summary>
		/// Loads the group to the current build.
		/// </summary>
		/// <param name="groupname">The groupname.</param>
		/// <returns></returns>
		public RuleEngine LoadGroupToBuild(string groupname)
		{
			try
			{
				var group = new BankTransactionGroup {Name = groupname};
				Db.Entry(Db.BankTransactionGroups.Attach(group)).State = System.Data.Entity.EntityState.Added;
				NewRuleBuild.ForEach(rule =>
				{
					rule.BankTransactionGroup = group;
				});
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
				ErrorManager.LogException(e);
			}
			catch (Exception e)
			{
				Exception = e;
				LogException(e);
			}
			return this;
		}

		#endregion

		#region Save

		/// <summary>
		/// Saves the specified rule.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <returns></returns>
		public bool Save(BankTransactionRule rule)
		{
			try
			{
				if (Db.Entry(rule).GetValidationResult().IsValid)
				{
					Db.Entry(Db.BankTransactionRules.Attach(rule)).State = System.Data.Entity.EntityState.Modified;
					Db.SaveChanges();
					return true;
				}
				return false;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
				ErrorManager.LogException(e);
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		/// <summary>
		/// Saves the specified rules. (with bulk insert SqlBulkCopy.)
		/// </summary>
		/// <param name="rules">The rules.</param>
		/// <returns></returns>
		public bool Save(List<BankTransactionRule> rules)
		{
			try
			{
				if (rules.Any(rule => !Db.Entry(rule).GetValidationResult().IsValid))
				{
					InsertBulk(rules.ToDataTable(Tables.BankTransactionRules.ToString()));
					return true;
				}
				return false;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
				ErrorManager.LogException(e);
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		/// <summary>
		/// Saves the new rules build. (with bulk insert SqlBulkCopy.)
		/// </summary>
		/// <returns></returns>
		public bool SaveNewRules()
		{
			try
			{
				if (NewRuleBuild.Any(rule => !Db.Entry(rule).GetValidationResult().IsValid))
				{
					InsertBulk(NewRuleBuild.ToDataTable(Tables.BankTransactionRules.ToString()));
					return true;
				}
				return false;
			}
			catch (DbEntityValidationException e)
			{
				Exception = e;
				ErrorManager.LogException(e);
				return false;
			}
			catch (Exception e)
			{
				Exception = e;
				return LogException(e);
			}
		}

		#endregion

		#region Build

		/// <summary>
		/// Build a new rule with text. 
		/// eg. "fee" or "bank fee" (for matching the word bank and fee in the same text)
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public RuleEngine Build(string text) 
		{
			NewRuleBuild.Add(new BankTransactionRule
			{
				Text = text,
				Description = text,
				RuleType = RuleType.Including,
			});
			return this;
		}

		/// <summary>
		/// Build a new rule with text and type. 
		/// eg. "fee" or "bank fee" (for matching the word bank and fee in the same text)
		/// </summary>
		/// <param name="text">The text.</param>
		/// <param name="type">The type.</param>
		/// <returns></returns>
		public RuleEngine Build(string text, RuleType type)
		{
			NewRuleBuild.Add(new BankTransactionRule
			{
				Text = text,
				Description = text,
				RuleType = type,
			});
			return this;
		}

		/// <summary>
		/// Build a new Include rule with text. 
		/// eg. "fee" or "bank fee" (for matching the word bank and fee in the same text)
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public RuleEngine Include(string text)
		{
			NewRuleBuild.Add(new BankTransactionRule
			{
			    Text = text,
				Description = text,
			    RuleType = RuleType.Including,
			});
			return this;
		}

		/// <summary>
		/// Build a new Exclude rule with text. 
		/// eg. "fee" or "bank fee" (for matching the word bank and fee in the same text)
		/// </summary>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public RuleEngine Exclude(string text)
		{
			NewRuleBuild.Add(new BankTransactionRule
			{
			    Text = text,
				Description = text,
			    RuleType = RuleType.Excluding,
			});
			return this;
		}

		#endregion
	}
}