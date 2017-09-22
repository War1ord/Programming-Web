using System;
using BudgetManager.Models;
using BudgetManager.Models.User;

namespace BudgetManager.Web.Models
{
    /// <summary>
    ///     The Class containing the Session Information.
    /// </summary>
    public class Session
    {
        /// <summary>
        ///     Gets or sets the user.
        /// </summary>
        /// <value>
        ///     The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        ///     Gets or sets the URI.
        /// </summary>
        /// <value>
        ///     The URI.
        /// </value>
        public Uri ReturnUrl { get; set; }
    }
}