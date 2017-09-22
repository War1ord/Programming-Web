using System.ComponentModel.DataAnnotations;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    public class ValidationViewModel : ViewModelBase
    {
        public string OneTimePin { get; set; }
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}