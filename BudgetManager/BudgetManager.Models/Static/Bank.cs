using BudgetManager.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.Models.Static
{
	/// <summary>
	/// The Bank Model
	/// </summary>
	public class Bank : IdModelBase
	{
		/// <summary>
		/// Gets or sets the banks name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[Required, StringLength(50), Display(Name = "Bank Name")]
		public string Name { get; set; }
		/// <summary>
		/// Gets or sets the banks description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[StringLength(500), Display(Name = "Bank Description")]
		public string Description { get; set; }
	}
}