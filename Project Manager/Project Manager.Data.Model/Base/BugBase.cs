using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Base
{
	public abstract class BugBase : ModelBase
	{
		[Required]
		public Guid BugId { get; set; }

		[ForeignKey(nameof(BugId))]
		public virtual Bug.Bug Bug { get; set; }
	}
}
