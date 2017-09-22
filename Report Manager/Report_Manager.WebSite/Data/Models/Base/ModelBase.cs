using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models.Base
{
	public abstract class ModelBase
	{
		private DateTime? dateTimeCreated;

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		public DateTime DateTimeCreated { get { return (dateTimeCreated ?? (dateTimeCreated = DateTime.Now)).Value; } set { dateTimeCreated = value; } }
		public DateTime? DateTimeModified { get; set; }

		[Required, ForeignKey(nameof(CreatedBy))]
		public int CreatedById { get; set; }
		[ForeignKey(nameof(ModifiedBy))]
		public int? ModifiedById { get; set; }

		public virtual User CreatedBy { get; set; }
		public virtual User ModifiedBy { get; set; }

		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }

		[NotMapped]
		public TimeSpan TimeSpanCreated { get { return DateTimeCreated - DateTime.Now; } }
		[NotMapped]
		public TimeSpan? TimeSpanModified { get { return DateTimeModified.HasValue ? (DateTimeModified.Value - DateTime.Now) : (TimeSpan?)null; } }

	}
}