using RequestForService.Models.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RequestForService.Models.BusinessEntities
{
	public class IndustryArea : CreatedByBase
	{
		private List<IndustryLevel> industryLevels;
		
		[Required]
		public string Name { get; set; }
		public string Description { get; set; }

		public List<IndustryLevel> IndustryLevels
		{
			get { return industryLevels ?? (industryLevels = new List<IndustryLevel>()); }
			set { industryLevels = value; }
		}
	}
}