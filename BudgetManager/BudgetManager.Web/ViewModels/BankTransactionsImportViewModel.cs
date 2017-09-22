using BudgetManager.Models.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    public class BankTransactionsImportViewModel : ViewModelBase
    {
        [Required]
        public int SelectedBankAccountId { get; set; }

        [Required]
        public IEnumerable<HttpPostedFileBase> Files { get; set; }

        public List<BankAccount> UserBankAccounts { get; set; }
    }
}