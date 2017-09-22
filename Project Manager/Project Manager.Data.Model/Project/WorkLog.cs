using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class WorkLog : Base.ModelBase
	{
		[Required]
		public Guid WorkItemId { get; set; }

		public string Description { get; set; }

		[Required]
		public TimeSpan TimeWorked { get; set; }

		[ForeignKey(nameof(WorkItemId))]
		public ProjectWorkItem WorkItem { get; set; }

	}
}