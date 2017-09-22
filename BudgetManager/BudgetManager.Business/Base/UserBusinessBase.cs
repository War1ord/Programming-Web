using BudgetManager.Models.User;

namespace BudgetManager.Business.Base
{
    public abstract class UserBusinessBase : BusinessBase
    {
        public User User { get; set; }

        protected UserBusinessBase(User user)
        {
            User = user;
        }
    }
}