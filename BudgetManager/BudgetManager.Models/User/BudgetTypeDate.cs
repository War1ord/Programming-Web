using System;
using System.Collections.Generic;
using BudgetManager.Enums;
using BudgetManager.Models.Base;

namespace BudgetManager.Models.User
{
	public class BudgetTypeDate : UserModelBase
	{
		#region Fields
		private List<BudgetRowItem> _budgetRowItems; 
		#endregion

		public DateTime StartDate { get; set; }
		public BudgetType BudgetType { get; set; }

		public List<BudgetRowItem> BudgetRowItems
		{
			get { return _budgetRowItems ?? (_budgetRowItems = new List<BudgetRowItem>()); }
			set { _budgetRowItems = value; }
		}
	}
}