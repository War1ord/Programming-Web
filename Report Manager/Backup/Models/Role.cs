using System.ComponentModel.DataAnnotations;

namespace Report_Manager.WebSite.Models
{
	public class Role : Base.ModelBase
	{
		[Required, StringLength(maximumLength: 200)]
		public string Name { get; set; }
	}
}