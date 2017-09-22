using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Bug
{
	public class BugAssignedUsers : Base.BugBase
	{
		[Required]
		public Guid AssignedUserId { get; set; }

		[ForeignKey(nameof(AssignedUserId))]
		public virtual User.User AssignedUser { get; set; }
	}
}