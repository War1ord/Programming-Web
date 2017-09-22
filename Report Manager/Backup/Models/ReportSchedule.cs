using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Models
{
	public class ReportSchedule : Base.ModelBase
	{
		private bool? isEnabled;

		[Required, ForeignKey(nameof(Report))]
		public int ReportId { get; set; }

		[Required]
		public Enums.IntervalType IntervalType { get; set; }

		[Required]
		public TimeSpan TimeSpanValue { get; set; }

		public DateTime? DateTimeStart { get; set; }

		public DateTime? DateTimeEnd { get; set; }

		public bool IsEnabled { get { return isEnabled ?? (isEnabled = true).Value; } set { isEnabled = value; } }

		public Report Report { get; set; }

	}
}