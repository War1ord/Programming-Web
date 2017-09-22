using BudgetManager.Enums;
using BudgetManager.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The Budget Template Item Model
	/// </summary>
	public class BudgetTemplateItem : UserModelBase
	{
		private List<BudgetRowItem> _budgetRowItems;

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[Required, StringLength(50)]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the type of the budget item.
		/// </summary>
		/// <value>
		/// The type of the budget item.
		/// </value>
		[EnumDataType(typeof (BudgetItemType))]
		public BudgetItemType BudgetItemType { get; set; }

		#region Navigation Properties

		/// <summary>
		/// Gets or sets the budget row items.
		/// </summary>
		/// <value>
		/// The budget row items.
		/// </value>
		public List<BudgetRowItem> BudgetRowItems
		{
			get { return _budgetRowItems ?? (_budgetRowItems = new List<BudgetRowItem>()); }
			set { _budgetRowItems = value; }
		}

		#endregion
	}
}