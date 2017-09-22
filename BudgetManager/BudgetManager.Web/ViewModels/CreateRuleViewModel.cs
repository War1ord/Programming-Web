using System.Collections.Generic;
using BudgetManager.Models;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    public class CreateRuleViewModel : ViewModelBase
    {
        public BankTransactionRule Rule { get; set; }
        public List<BankAccount> Accounts { get; set; }
        public List<BankTransactionGroup> Groups { get; set; }
    }
}