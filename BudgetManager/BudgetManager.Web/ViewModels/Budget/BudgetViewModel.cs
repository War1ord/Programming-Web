using System;
using System.Collections.Generic;
using BudgetManager.Models.User;
using BudgetManager.Web.Base;

namespace BudgetManager.Web.ViewModels.Budget
{
    /// <summary>
    /// 
    /// </summary>
    public class BudgetViewModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
        /// <summary>
        /// Gets or sets the budget template items.
        /// </summary>
        /// <value>
        /// The budget template items.
        /// </value>
        public List<BudgetTemplateItem> BudgetTemplateItems { get; set; }
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        /// <value>
        /// The start date.
        /// </value>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        /// <value>
        /// The end date.
        /// </value>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Gets or sets the budget type dates.
        /// </summary>
        /// <value>
        /// The budget type dates.
        /// </value>
        public List<BudgetTypeDate> BudgetTypeDates { get; set; }
    }
}