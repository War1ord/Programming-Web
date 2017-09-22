using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectWorkFlow : Base.ProjectBase
	{
		[Required]
		public Guid StepId { get; set; }

		[ForeignKey(nameof(StepId))]
		public WorkFlowStep Step { get; set; }

	}
}