namespace BudgetManager.Common.Messages
{
	public static class BankTransactionMessages
	{
		public static readonly string UserNotSet = "The user was not set.";

		public static readonly string UserNew = "The user seem to be a new and not saved user. ";

		public static readonly string BankTransactionsNotExistsAnyNew = "There is not any new bank transactions.";

		public static readonly string BankTransactionsNotSavedSomeOrAll = "Some or all of the bank transactions did not save.";

		public static readonly string BankTransactionsSaved = "Bank Transaction(s) saved.";

		public static readonly string BankTransactionsRemoveDuplicatesFailed = "The Removal of your duplicate bank trasactions failed.";

		public static readonly string BankTransactionsRemoveDuplicatesNoDuplicates = "There was no duplicates to remove.";

		public static readonly string RuleSaved = "The rule was saved.";

		public static readonly string RuleNotSaved = "The rule not saved.";
	}
}