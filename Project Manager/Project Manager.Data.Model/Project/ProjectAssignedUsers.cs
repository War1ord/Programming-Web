using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectAssignedUsers : Base.ProjectBase
	{
		[Required]
		public Guid AssignedUserId { get; set; }

		[ForeignKey(nameof(AssignedUserId))]
		public virtual User.User AssignedUser { get; set; }
	}
}