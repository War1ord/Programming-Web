using BudgetManager.Enums;
using BudgetManager.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The Bank Transaction Rules
	/// </summary>
	public class BankTransactionRule : UserModelBase
	{
        /// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
        [Required, StringLength(500)]
		public string Description { get; set; }
		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		/// <value>
		/// The text.
		/// </value>
        [Required, Display(Name = "Text", Description = "Text Rule of the Bank Transaction")]
		public string Text { get; set; }
		/// <summary>
		/// Gets or sets the type of the rule.
		/// </summary>
		/// <value>
		/// The type of the rule.
		/// </value>
        [Required, Display(Name = "Type", Description = "Type of the Bank Transaction")]
		public RuleType RuleType { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this rule is case sensitive.
		/// </summary>
		/// <value>
		///   <c>true</c> if [is case sensitive]; otherwise, <c>false</c>.
		/// </value>
		public bool IsCaseSensitive { get; set; }
		/// <summary>
		/// Gets or sets the bank account id.
		/// </summary>
		/// <value>
		/// The bank account id.
		/// </value>
        [Required, Display(Name = "Bank Account", Description = "Bank Account of the Bank Transaction")]
		public Guid BankAccountId { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction group id.
		/// </summary>
		/// <value>
		/// The bank transaction group id.
		/// </value>
        [Required, Display(Name = "Group", Description = "Grouping of the Bank Transaction")]
		public Guid BankTransactionGroupId { get; set; }

		#region ForeignKeys
		/// <summary>
		/// Gets or sets the bank account.
		/// </summary>
		/// <value>
		/// The bank account.
		/// </value>
		[ForeignKey("BankAccountId")]
		public BankAccount BankAccount { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction group.
		/// </summary>
		/// <value>
		/// The bank transaction group.
		/// </value>
		[ForeignKey("BankTransactionGroupId")]
		public BankTransactionGroup BankTransactionGroup { get; set; }
		#endregion
	}
}