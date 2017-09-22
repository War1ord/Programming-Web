using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class RelatedProjectLink : Base.ProjectBase
	{
		[Required]
		public Guid RelatedProjectId { get; set; }

		[ForeignKey(nameof(RelatedProjectId))]
		public virtual Project RelatedProject { get; set; }
	}
}