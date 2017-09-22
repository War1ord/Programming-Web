using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class Report : Base.ModelBase
	{
		[ForeignKey(nameof(DatabaseConnection))]
		public int DatabaseConnectionId { get; set; }

		[Required, Index(IsUnique = true), StringLength(maximumLength: 200)]
		public string Name { get; set; }

		[Required]
		public string Query { get; set; }

		[Required]
		public bool IsToBeEmailed { get; set; }

		public ComplexTypes.Email Email { get; set; }

		[Required]
		public bool IsToBeSavedToFile { get; set; }

		[Required]
		public string FullFilePath { get; set; }

		public bool IsReportTemplate { get; set; }

		public string Template { get; set; }

		public Enums.FileType FileType { get; set; }

		public DatabaseConnection DatabaseConnection { get; set; }

	}
}