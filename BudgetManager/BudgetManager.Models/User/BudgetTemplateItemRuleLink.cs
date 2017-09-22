using BudgetManager.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The link between BudgetTemplateItem and BankTransactionRule
	/// </summary>
	public class BudgetTemplateItemRuleLink : UserModelBase
	{
		/// <summary>
		/// Gets or sets the budget template item id.
		/// </summary>
		/// <value>
		/// The budget template item id.
		/// </value>
		[Required]
		public Guid BudgetTemplateItemId { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction rule id.
		/// </summary>
		/// <value>
		/// The bank transaction rule id.
		/// </value>
		[Required]
		public Guid BankTransactionRuleId { get; set; }

		#region ForeignKeys
		/// <summary>
		/// Gets or sets the budget template item.
		/// </summary>
		/// <value>
		/// The budget template item.
		/// </value>
		[ForeignKey("BudgetTemplateItemId")]
		public BudgetTemplateItem BudgetTemplateItem { get; set; }
		/// <summary>
		/// Gets or sets the bank transaction rule.
		/// </summary>
		/// <value>
		/// The bank transaction rule.
		/// </value>
		[ForeignKey("BankTransactionRuleId")]
		public BankTransactionRule BankTransactionRule { get; set; }
		#endregion
	}
}