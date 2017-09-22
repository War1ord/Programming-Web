using BudgetManager.Extentions;
using BudgetManager.Models.Base;
using BudgetManager.Models.ComplexTypes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetManager.Models.User
{
	/// <summary>
	/// The User Model
	/// </summary>
	public class User : IdModelBase
	{
		#region Fields

		/// <summary>
		/// The _password hash
		/// </summary>
		private string _passwordHash;
		/// <summary>
		/// The _person
		/// </summary>
		private Person _person;

		#endregion

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		/// <value>
		/// The email.
		/// </value>
		[Required]
		public string Email { get; set; }
		/// <summary>
		/// Gets the password hash.
		/// </summary>
		/// <value>
		/// The password hash.
		/// </value>
		[Required]
		public string PasswordHash
		{
			get
			{
				return string.IsNullOrWhiteSpace(_passwordHash)
						   ? Password.ToPasswordHash()
						   : _passwordHash;
			}
			private set { _passwordHash = value; }
		}
		/// <summary>
		/// Gets or sets the person.
		/// </summary>
		/// <value>
		/// The person.
		/// </value>
		public Person Person
		{
			get { return _person ?? new Person(); }
			set { _person = value; }
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance is validated.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is validated; otherwise, <c>false</c>.
		/// </value>
        public string OneTimePin { get; set; }
        [Required]
		public bool IsValidated { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is authenticated.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
		/// </value>
		[Required]
		public bool IsAuthenticated { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is locked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is locked; otherwise, <c>false</c>.
		/// </value>
		[Required]
		public bool IsLocked { get; set; }
		/// <summary>
		/// Gets or sets a value indicating whether this instance is admin.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is admin; otherwise, <c>false</c>.
		/// </value>
		public bool IsAdmin { get; set; }

		#region Not Mapped

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>
		/// The password.
		/// </value>
		[NotMapped, DataType(DataType.Password)]
		public string Password { get; set; }

		#endregion

		#region Lists

		/// <summary>
		/// Gets or sets the bank accounts.
		/// </summary>
		/// <value>
		/// The bank accounts.
		/// </value>
		public List<BankAccount> BankAccounts { get; set; }
		/// <summary>
		/// Gets or sets the bank transactions.
		/// </summary>
		/// <value>
		/// The bank transactions.
		/// </value>
		public List<BankTransaction> BankTransactions { get; set; }

		#endregion

    }
}