using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Bug
{
	public class BugNote : Base.BugBase
	{
		[Required]
		public string Text { get; set; }
	}
}