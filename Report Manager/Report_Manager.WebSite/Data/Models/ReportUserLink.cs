using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class ReportUserLink : Base.ModelBase
	{
		[Required, ForeignKey(nameof(Report))]
		public int ReportId { get; set; }

		[Required, ForeignKey(nameof(User))]
		public int UserId { get; set; }

		public User User { get; set; }

		public Report Report { get; set; }

	}
}