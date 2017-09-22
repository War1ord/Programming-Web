using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class Error : Base.ModelBase
	{

		[ForeignKey(nameof(InnerException))]
		public int? InnerExceptionId { get; set; }

		public string HelpLink { get; set; }
		public int HResult { get; set; }
		public string Message { get; set; }
		public string Source { get; set; }
		public string StackTrace { get; set; }

		public Error InnerException { get; set; }

	}
}