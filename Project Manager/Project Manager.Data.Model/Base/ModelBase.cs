using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Base
{
	[Serializable]
	public abstract class ModelBase
	{
		#region Fields
		private Guid? _id;
		private DateTime? _dateCreated;
		#endregion

		[Key, Display(Name = "Identifier"), Column(Order = 0)] /*(removed because the id property should be generated at entity initializations) DatabaseGenerated(DatabaseGeneratedOption.Identity)*/
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

		[Display(Name = "Is Active")]
		public bool IsActive { get; set; }

		[Display(Name = "Is Deleted")]
		public bool IsDeleted { get; set; }

		[Required, Display(Name = "Created By")]
		public Guid CreatedByUserId { get; set; }

		[ForeignKey(nameof(CreatedByUserId))]
		public User.User CreatedByUser { get; set; }

		[NotMapped, Display(Name = "Age")]
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