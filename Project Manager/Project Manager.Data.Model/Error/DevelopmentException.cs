using System.ComponentModel.DataAnnotations;

namespace Project_Manager.Data.Model.Error
{
	public class DevelopmentException : Base.ModelBase
	{
		[Required]
		public string Message { get; set; }

		public string StackTrace { get; set; }

		public string CodeFile { get; set; }

		public int? CodeLine { get; set; }

	}
}
