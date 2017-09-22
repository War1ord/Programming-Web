using Project_Manager.Data.Model.Manage;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Bug
{
	public class BugTagLink : Base.BugBase
	{
		[Required]
		public Guid TagId { get; set; }

		[ForeignKey(nameof(TagId))]
		public virtual Tag Tag { get; set; }
	}
}