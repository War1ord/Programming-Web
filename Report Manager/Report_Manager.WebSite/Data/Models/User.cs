using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Data.Models
{
	public class User : Base.ModelBase
	{
		[Required, Index(IsUnique = true)]
		public string Username { get; set; }

		[Required]
		public string Password { get; set; }

		public bool IsUsernameEmail { get; set; }

	}
}