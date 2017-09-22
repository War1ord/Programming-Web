using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.User
{
	public class UserAdmin : Base.ModelBase
	{
		[Required]
		public Guid UserId { get; set; }

		[ForeignKey(nameof(UserId))]
		public virtual User User { get; set; }

	}
}
