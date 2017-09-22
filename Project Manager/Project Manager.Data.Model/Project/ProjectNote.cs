using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectNote : Base.ProjectBase
	{
		[Required]
		public string Text { get; set; }
	}
}