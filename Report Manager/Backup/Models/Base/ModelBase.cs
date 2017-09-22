using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Models.Base
{
	public abstract class ModelBase
	{
		//private Guid? id;
		private DateTime? dateTimeCreated;

		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		//public Guid Id { get { return (id.HasValue && id != Guid.Empty ? id : (id = Guid.NewGuid())).Value; } set { id = value; } }
		public int Id { get; set; }

		[Required]
		public DateTime DateTimeCreated { get { return (dateTimeCreated ?? (dateTimeCreated = DateTime.Now)).Value; } set { dateTimeCreated = value; } }

		[Required, ForeignKey(nameof(CreatedBy))]
		public Guid CreatedById { get; set; }

		[Required]
		public DateTime DateTimeModified { get; set; }

		[Required, ForeignKey(nameof(ModifiedBy))]
		public Guid ModifiedById { get; set; }

		public User CreatedBy { get; set; }
		public User ModifiedBy { get; set; }
	}
}