using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Project
{
	public class WorkFlowStep : Base.ModelBase
	{
		[Required]
		public int Seq { get; set; }
		[Required]
		public string Name { get; set; }
	}
}