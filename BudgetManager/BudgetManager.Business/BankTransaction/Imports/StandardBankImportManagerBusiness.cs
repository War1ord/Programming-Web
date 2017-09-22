using BudgetManager.Models.User;

namespace BudgetManager.Business.BankTransaction.Imports
{
	/// <summary>
	/// The Standard Bank Transaction Import Manager
	/// </summary>
	public class StandardBankImportManagerBusiness : ImportManagerBusinessBase
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="StandardBankImportManagerBusiness"/> class.
		/// </summary>
		/// <param name="folderPath">The folder path.</param>
		/// <param name="extension">The extension.</param>
		/// <param name="user">The user.</param>
		public StandardBankImportManagerBusiness(string folderPath, string extension, User user) : base(folderPath, extension, user)
		{
		}

		#endregion
	}
}