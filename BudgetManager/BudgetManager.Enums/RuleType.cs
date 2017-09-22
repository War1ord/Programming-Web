using System.ComponentModel;

namespace BudgetManager.Enums
{
	public enum RuleType
	{
		/// <summary>
		/// The rule type specifying that this rule includes the text.
		/// </summary>
		Including,
		/// <summary>
		/// The rule type specifying that this rule excludes the text.
		/// </summary>
		Excluding,
	}
}