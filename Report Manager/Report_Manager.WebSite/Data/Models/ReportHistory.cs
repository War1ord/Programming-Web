using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class ReportHistory : Base.ModelBase
	{
		[Required, ForeignKey(nameof(Report))]
		public int ReportId { get; set; }

		[Required]
		public Enums.ReportAction Action { get; set; }

		public Report Report { get; set; }

	}
}