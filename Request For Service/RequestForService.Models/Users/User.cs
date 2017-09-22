using RequestForService.Security.Passwords;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Users
{
	public class User : Base.IdBase
	{
		private string passwordHash;
		private ComplexTypes.Person person;
		private ComplexTypes.ContactDetails contactDetails;
		private ComplexTypes.JobDetails jobDetails;

		[Required, EmailAddress]
		[Display(Name = "Email")]
		public string EmailAddress { get; set; }
		public string PasswordHash
		{
			get
			{
				return !string.IsNullOrWhiteSpace(Password)
					? Password.ToPasswordHash()
					: passwordHash;
			}
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					passwordHash = value;
				}
			}
		}

		[Display(Name = "")]
		public bool IsLocked { get; set; }

		[Display(Name = "Receive Newsletters")]
		public bool ReceiveNewsletters { get; set; }

		/* Complex Types */
		public ComplexTypes.Person Person
		{
			get { return person ?? (person = new ComplexTypes.Person()); }
			set { person = value; }
		}
		public ComplexTypes.ContactDetails ContactDetails
		{
			get { return contactDetails ?? (contactDetails = new ComplexTypes.ContactDetails()); }
			set { contactDetails = value; }
		}
		public ComplexTypes.JobDetails JobDetails
		{
			get { return jobDetails ?? (jobDetails = new ComplexTypes.JobDetails()); }
			set { jobDetails = value; }
		}

		[Display(Name = "Business Entity")]
		public Guid? BusinessEntityId { get; set; }

		[ForeignKey("BusinessEntityId")]
		public BusinessEntities.BusinessEntity BusinessEntity { get; set; }

		[NotMapped, PasswordPropertyText]
		public string Password { get; set; }

	}
}