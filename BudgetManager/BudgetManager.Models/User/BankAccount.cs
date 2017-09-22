using BudgetManager.Models.Base;
using BudgetManager.Models.Static;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The BankAccount Model
	/// </summary>
	public class BankAccount : UserModelBase
	{
		/// <summary>
		/// Gets or sets the bank id.
		/// </summary>
		/// <value>
		/// The bank id.
		/// </value>
		[Required]
		public Guid BankId { get; set; }
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[Required]
		[Display(Name = "Bank Account")]
		[StringLength(50)]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[Display(Name = "Description")]
		[StringLength(500)]
		public string Description { get; set; }

		#region ForeignKeys
		/// <summary>
		/// Gets or sets the bank.
		/// </summary>
		/// <value>
		/// The bank.
		/// </value>
		[ForeignKey("BankId")]
		[Display(Name = "Bank")]
		public Bank Bank { get; set; }
		#endregion

		#region Lists

		/// <summary>
		/// Gets or sets the bank transactions.
		/// </summary>
		/// <value>
		/// The bank transactions.
		/// </value>
		public List<BankTransaction> BankTransactions { get; set; }

		#endregion
	}
}