using System.Collections.Generic;
using BudgetManager.Models;
using BudgetManager.Models.Static;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    /// <summary>
    ///     The Bank Transactions View Model
    /// </summary>
    public class BankTransactionsViewModel : ViewModelBase
    {
        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        /// <value>
        ///     The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        ///     Gets or sets the page count.
        /// </summary>
        /// <value>
        ///     The page count.
        /// </value>
        public int PageCount { get; set; }

        /// <summary>
        ///     Gets or sets the size of the page.
        /// </summary>
        /// <value>
        ///     The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        ///     Gets or sets the total item count.
        /// </summary>
        /// <value>
        ///     The total item count.
        /// </value>
        public int TotalItemCount { get; set; }

        /// <summary>
        ///     Gets or sets the text search.
        /// </summary>
        /// <value>
        ///     The text search.
        /// </value>
        public string TextSearch { get; set; }

        #region Lists

        /// <summary>
        ///     Gets or sets the bank transactions.
        /// </summary>
        /// <value>
        ///     The bank transactions.
        /// </value>
        public List<BankTransaction> BankTransactions { get; set; }

        /// <summary>
        ///     Gets or sets the banks.
        /// </summary>
        /// <value>
        ///     The banks.
        /// </value>
        public List<Bank> Banks { get; set; }

        #endregion
    }
}