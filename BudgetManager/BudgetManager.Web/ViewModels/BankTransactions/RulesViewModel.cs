using System.Collections.Generic;
using BudgetManager.Models;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels.BankTransactions
{
    public class RulesViewModel : ViewModelBase
    {
        public List<BankTransactionRule> Rules { get; set; }
    }
}