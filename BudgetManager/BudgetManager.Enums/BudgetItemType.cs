using System;

namespace BudgetManager.Enums
{
	[Serializable]
	public enum BudgetItemType
	{
		ExpenseFixed = 1,
		ExpenseVariable = 2,
		Income = 3,
	}
}