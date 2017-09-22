using System.Collections.Generic;
using BudgetManager.Models;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels.BankTransactions
{
    public class GroupViewModel : ViewModelBase
    {
        public List<BankTransactionGroup> Groups { get; set; }
    }
}