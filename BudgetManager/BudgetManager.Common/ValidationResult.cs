using System.Collections.Generic;

namespace BudgetManager.Common
{
	public class ValidationResult
	{
		#region Fields

		private Dictionary<string, string> _errors;

		#endregion

		#region Properties

		public Dictionary<string, string> ValidationErrors
		{
			get { return _errors ?? (_errors = new Dictionary<string, string>()); }
		}

		public bool IsValid { get; set; }

		#endregion
	}
}