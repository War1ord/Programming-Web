using BudgetManager.Models.User;

namespace BudgetManager.Business.BankTransaction.Imports
{
	public class FirstNationalBankImportManagerBusiness : ImportManagerBusinessBase
	{
		#region Constructors

		public FirstNationalBankImportManagerBusiness(string folderPath, string extension, User user) : base(folderPath, extension, user)
		{
		}

		#endregion
	}
}