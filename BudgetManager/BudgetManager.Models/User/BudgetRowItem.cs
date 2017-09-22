using BudgetManager.Models.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The BudgetRow Item Model
	/// </summary>
	public class BudgetRowItem : UserModelBase
	{
		/// <summary>
		/// Gets or sets the budget date.
		/// </summary>
		/// <value>
		/// The budget date.
		/// </value>
		[Required, Display(Name = "Budget Date"), DataType(DataType.DateTime)]
		public DateTime BudgetDate { get; set; }
		/// <summary>
		/// Gets or sets the amount budget.
		/// </summary>
		/// <value>
		/// The amount budget.
		/// </value>
		[Required, Display(Name = "Budget Amount"), DataType(DataType.Currency)]
		public decimal AmountBudget { get; set; }
		/// <summary>
		/// Gets or sets the amount actual.
		/// </summary>
		/// <value>
		/// The amount actual.
		/// </value>
		[DataType(DataType.Currency), Display(Name = "Actual Amount")]
		public decimal AmountActual { get; set; }

		/// <summary>
		/// Gets or sets the budget template item id.
		/// </summary>
		/// <value>
		/// The budget template item id.
		/// </value>
		[Required]
		public Guid BudgetTemplateItemId { get; set; }
		[Required]
		public Guid BudgetTypeDateId { get; set; }

		#region Navigation
		/// <summary>
		/// Gets or sets the budget template item.
		/// </summary>
		/// <value>
		/// The budget template item.
		/// </value>
		[ForeignKey("BudgetTemplateItemId"), Display(Name = "Budget Template Item")]
		public BudgetTemplateItem BudgetTemplateItem { get; set; }
		[ForeignKey("BudgetTypeDateId"), Display(Name = "Budget Type for Date")]
		public BudgetTypeDate BudgetTypeDate { get; set; }
		#endregion
	}
}