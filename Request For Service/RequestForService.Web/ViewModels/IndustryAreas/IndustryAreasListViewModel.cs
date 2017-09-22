using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.IndustryAreas
{
	public class IndustryAreasListViewModel : Base.ViewModelBase
	{
		public List<RequestForService.Models.BusinessEntities.IndustryArea> List { get; set; }
	}
}