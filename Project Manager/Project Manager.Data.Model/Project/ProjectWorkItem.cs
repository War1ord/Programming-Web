using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectWorkItem : Base.ProjectBase
	{
		[Required]
		public string Description { get; set; }

		public TimeSpan? Estimation { get; set; }

		public virtual List<WorkLog> WorkLogs { get; set; }
	}
}