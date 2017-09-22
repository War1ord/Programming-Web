using Project_Manager.Data.Model.Manage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectTagLink : Base.ProjectBase
	{
		[Required]
		public Guid TagId { get; set; }

		[ForeignKey(nameof(TagId))]
		public virtual Tag Tag { get; set; }
	}
}