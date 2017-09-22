using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.ComplexTypes
{
	/// <summary>
	/// The ContactDetail Class
	/// </summary>
	[ComplexType]
	public class ContactDetail
	{
		/// <summary>
		/// Gets or sets the mobile telephone number.
		/// </summary>
		/// <value>
		/// The mobile telephone number.
		/// </value>
		[Display(Name = "Mobile Telephone Number", Description = "Mobile number of the person.", Prompt = "Please enter a mobile telephone number of the person.")]
		[DataType(DataType.PhoneNumber)]
		public string MobileTelephoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the home telephone number.
		/// </summary>
		/// <value>
		/// The home telephone number.
		/// </value>
		[Display(Name = "Home Telephone Number", Description = "Home number of the person.", Prompt = "Please enter a home telephone number of the person.")]
		[DataType(DataType.PhoneNumber)]
		public string HomeTelephoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the work telephone number.
		/// </summary>
		/// <value>
		/// The work telephone number.
		/// </value>
		[Display(Name = "Work Telephone Number", Description = "Work number of the person.", Prompt = "Please provide a work telephone number of the person.")]
		[DataType(DataType.PhoneNumber)]
		public string WorkTelephoneNumber { get; set; }
		/// <summary>
		/// Gets or sets the fax number.
		/// </summary>
		/// <value>
		/// The fax number.
		/// </value>
		[Display(Name = "Fax", Description = "Title of the person.", Prompt = "Please provide a title.")]
		[DataType(DataType.PhoneNumber)]
		public string FaxNumber { get; set; }
	}
}