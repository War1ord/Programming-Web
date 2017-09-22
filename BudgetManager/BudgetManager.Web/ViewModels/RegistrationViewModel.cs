using System.ComponentModel.DataAnnotations;
using BudgetManager.Models;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels
{
    public class RegistrationViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        ///     The password
        /// </summary>
        private string _password;

        #endregion

        /// <summary>
        ///     Gets or sets the password.
        /// </summary>
        /// <value>
        ///     The password.
        /// </value>
        [Required]
        [DataType(DataType.Password)]
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                if (User != null)
                    User.Password = value;
            }
        }

        /// <summary>
        ///     Gets or sets the confirm password.
        /// </summary>
        /// <value>
        ///     The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        public User User { get; set; }
    }
}