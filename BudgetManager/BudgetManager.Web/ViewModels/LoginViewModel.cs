using System.ComponentModel.DataAnnotations;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}