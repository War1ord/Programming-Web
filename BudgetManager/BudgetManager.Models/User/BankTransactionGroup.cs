using BudgetManager.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The Bank Transaction Group Model
	/// </summary>
	[Serializable]
	public class BankTransactionGroup : UserModelBase
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[Required, StringLength(50), Display(Name = "Group")]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[StringLength(500)]
		public string Description { get; set; }
    }
}