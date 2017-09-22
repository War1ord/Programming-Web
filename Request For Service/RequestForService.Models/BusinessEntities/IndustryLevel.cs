using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.BusinessEntities
{
	public class IndustryLevel : Base.CreatedByBase
	{
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		[Required]
		[Display(Name = "Level Order")]
		public int LevelOrder { get; set; }

		[Required]
		[Display(Name = "Industry Area")]
		public Guid IndustryAreaId { get; set; }

		[ForeignKey("IndustryAreaId")]
		public IndustryArea IndustryArea { get; set; }

	}
}