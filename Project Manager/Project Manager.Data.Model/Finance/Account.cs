using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Finance
{
	public class Account : Base.ModelBase
	{
		[Required]
		public Guid AccountUserId { get; set; }

		public string Name { get; set; }

		[ForeignKey(nameof(AccountUserId))]
		public virtual User.User AccountUser { get; set; }
	}
}