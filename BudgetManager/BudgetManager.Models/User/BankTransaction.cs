using BudgetManager.Enums;
using BudgetManager.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The Bank Transaction Model
	/// </summary>
	public class BankTransaction : UserModelBase
	{
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[Required, StringLength(500), Display(Name = "Description")]
		public string Description { get; set; }
		/// <summary>
		/// Gets or sets the transaction date.
		/// </summary>
		/// <value>
		/// The transaction date.
		/// </value>
		[Required, Display(Name = "Transaction Date")]
		public DateTime TransactionDate { get; set; }
		/// <summary>
		/// Gets or sets the amount.
		/// </summary>
		/// <value>
		/// The amount.
		/// </value>
		[Required, Display(Name = "Amount"), DataType(DataType.Currency)]
		public double Amount { get; set; }
		/// <summary>
		/// Gets or sets the balance.
		/// </summary>
		/// <value>
		/// The balance.
		/// </value>
		[Required, Display(Name = "Balance"), DataType(DataType.Currency)]
		public double Balance { get; set; }
		/// <summary>
		/// Gets or sets the transaction sequence.
		/// </summary>
		/// <value>
		/// The transaction sequence.
		/// </value>
		[Required, Display(Name = "Transaction Sequence")]
		public int TransactionSequence { get; set; }
		/// <summary>
		/// Gets or sets the type of the bank transaction.
		/// </summary>
		/// <value>
		/// The type of the bank transaction.
		/// </value>
		[Display(Name = "Bank Transaction Type"), EnumDataType(typeof(BankTransactionType))]
		public BankTransactionType BankTransactionType { get; set; }
		/// <summary>
		/// Gets or sets the type group id.
		/// </summary>
		/// <value>
		/// The type group id.
		/// </value>
		public Guid? GroupId { get; set; }
		/// <summary>
		/// Gets or sets the bank account id.
		/// </summary>
		/// <value>
		/// The bank account id.
		/// </value>
		public Guid? BankAccountId { get; set; }

		#region ForeignKeys
		/// <summary>
		/// Gets or sets the bank transaction group.
		/// </summary>
		/// <value>
		/// The bank transaction group.
		/// </value>
		[ForeignKey("GroupId"), Display(Name = "Bank Transaction Group")]
		public BankTransactionGroup BankTransactionGroup { get; set; }
		/// <summary>
		/// Gets or sets the bank account.
		/// </summary>
		/// <value>
		/// The bank account.
		/// </value>
		[ForeignKey("BankAccountId"), Display(Name = "Bank Account")]
		public BankAccount BankAccount { get; set; }
		#endregion

	}
}