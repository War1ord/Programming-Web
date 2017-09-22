using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Report_Manager.WebSite.Models
{
	public class UserRole : Base.ModelBase
	{
		[Required, ForeignKey(nameof(User))]
		public int UserId { get; set; }

		[Required, ForeignKey(nameof(Role))]
		public int RoleId { get; set; }

		public User User { get; set; }

		public Role Role { get; set; }

	}
}