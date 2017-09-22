using BudgetManager.Models;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels.BankTransactions
{
    public class AddGroupViewModel : ViewModelBase
    {
        public BankTransactionGroup BankTransactionGroup { get; set; }
    }
}