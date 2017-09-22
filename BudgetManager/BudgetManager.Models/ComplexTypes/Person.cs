using BudgetManager.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.ComplexTypes
{
	/// <summary>
	/// The Person Class
	/// </summary>
	[ComplexType]
	public class Person
	{
		#region Fields

		/// <summary>
		/// The _contact detail
		/// </summary>
		private ContactDetail _contactDetail;
		/// <summary>
		/// The _address info home
		/// </summary>
		private AddressInfo _addressInfoHome;
		/// <summary>
		/// The _address info work
		/// </summary>
		private AddressInfo _addressInfoWork;
		/// <summary>
		/// The name format
		/// </summary>
        [NotMapped]
        private const string NameFormat = "{0} {1}";

		#endregion

		/// <summary>
		/// Gets or sets the gender.
		/// </summary>
		/// <value>
		/// The gender.
		/// </value>
		[Display(Name = "Gender", Description = "Gender of the person.", Prompt = "Please provide a gender of the person.")]
		[EnumDataType(typeof (Gender))]
		public Gender? Gender { get; set; }
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>
		/// The title.
		/// </value>
		[Display(Name = "Title", Description = "Title of the person.", Prompt = "Please provide a title.")]
		[EnumDataType(typeof (Title))]
		public Title? Title { get; set; }
		/// <summary>
		/// Gets or sets the title other.
		/// </summary>
		/// <value>
		/// The title other.
		/// </value>
		[Display(Name = "Title Other", Description = "If there is another Title provide.",
			Prompt = "Please enter another title.")]
		public string TitleOther { get; set; }
		/// <summary>
		/// Gets or sets the first name.
		/// </summary>
		/// <value>
		/// The first name.
		/// </value>
		[Display(Name = "First Name", Description = "First Name of the person.",
			Prompt = "Please enter the First Name of the person.")]
		public string FirstName { get; set; }
		/// <summary>
		/// Gets or sets the middle names.
		/// </summary>
		/// <value>
		/// The middle names.
		/// </value>
		[Display(Name = "Middle Names", Description = "Middle Names of the person.",
			Prompt = "Please enter the Middle Names of the person.")]
		public string MiddleNames { get; set; }
		/// <summary>
		/// Gets or sets the name of the nick.
		/// </summary>
		/// <value>
		/// The name of the nick.
		/// </value>
		[Required]
		[Display(Name = "Nick Name", Description = "NickName of the person.",
			Prompt = "Please enter a nick name of the person.")]
		public string NickName { get; set; }
		/// <summary>
		/// Gets or sets the surname.
		/// </summary>
		/// <value>
		/// The surname.
		/// </value>
		[Required]
		[Display(Name = "Surname", Description = "Surname of the person.", Prompt = "Please enter a surname of the person.")]
		public string Surname { get; set; }

		#region NotMapped

		/// <summary>
		/// Gets the full name, formatted.
		/// </summary>
		/// <value>
		/// The full name.
		/// </value>
		[NotMapped]
		public string FullName { get { return string.Format(NameFormat, FirstName, Surname); } }
		/// <summary>
		/// Gets the full nick name, formatted.
		/// </summary>
		/// <value>
		/// The full nick name.
		/// </value>
		[NotMapped]
		public string FullNickName { get { return string.Format(NameFormat, NickName, Surname); } }

		#endregion

		#region Complex Types

		/// <summary>
		/// Gets or sets the contact detail.
		/// </summary>
		/// <value>
		/// The contact detail.
		/// </value>
		public ContactDetail ContactDetail
		{
			get { return _contactDetail ?? new ContactDetail(); }
			set { _contactDetail = value; }
		}
		/// <summary>
		/// Gets or sets the address info home.
		/// </summary>
		/// <value>
		/// The address info home.
		/// </value>
		public AddressInfo AddressInfoHome
		{
			get { return _addressInfoHome ?? new AddressInfo(); }
			set { _addressInfoHome = value; }
		}
		/// <summary>
		/// Gets or sets the address info work.
		/// </summary>
		/// <value>
		/// The address info work.
		/// </value>
		public AddressInfo AddressInfoWork
		{
			get { return _addressInfoWork ?? new AddressInfo(); }
			set { _addressInfoWork = value; }
		}

		#endregion
	}
}