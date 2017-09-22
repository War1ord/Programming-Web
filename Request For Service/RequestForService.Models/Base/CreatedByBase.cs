using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class CreatedByBase : IdBase
	{
		[Required]
		[Display(Name = "Created By")]
		public Guid CreatedByUserId { get; set; }

		[ForeignKey("CreatedByUserId")]
		public Users.User CreatedByUser { get; set; }
	}
}