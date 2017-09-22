using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class DatabaseConnection : Base.ModelBase
	{
		[Required, Index(IsUnique = true), StringLength(maximumLength: 200)]
		public string Name { get; set; }

		[Required, StringLength(maximumLength: 200)]
		public string Provider { get; set; }

		[Required]
		public string ConnectionString { get; set; }

	}
}