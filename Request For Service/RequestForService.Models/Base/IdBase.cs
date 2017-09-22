using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class IdBase : ModelBase
	{
		private Guid? _id;
		private DateTime? _dateCreated;

		[Key, Column(Order = 0)] /*(removed because the id property should be generated at entity initializations) DatabaseGenerated(DatabaseGeneratedOption.Identity)*/
		[Display(Name = "Identifier")]
		public Guid Id
		{
			get { return (_id ?? (_id = Guid.NewGuid())).Value; }
			set { _id = value; }
		}
		[Display(Name = "Date Created")]
		public DateTime DateCreated
		{
			get { return (_dateCreated ?? (_dateCreated = DateTime.Now)).Value; }
			set { _dateCreated = value; }
		}

		[Display(Name = "Is Deleted")]
		public bool IsDeleted { get; set; }

		[NotMapped]
		[Display(Name = "Age")]
		public TimeSpan Age
		{
			get { return GetAgeByDate(DateTime.Now); }
		}
		public TimeSpan GetAgeByDate(DateTime datetime)
		{
			return datetime - DateCreated;
		}
	}
}