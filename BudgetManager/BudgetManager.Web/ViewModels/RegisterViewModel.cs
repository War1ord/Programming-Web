using BudgetManager.Models;
using BudgetManager.Models.User;

namespace BudgetManager.Web.ViewModels
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public string Error { get; set; }
    }
}