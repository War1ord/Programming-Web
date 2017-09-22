using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Base
{
	public abstract class ProjectBase : ModelBase
	{
		[Required]
		public Guid ProjectId { get; set; }

		[ForeignKey(nameof(ProjectId))]
		public virtual Project.Project Project { get; set; }
	}
}
