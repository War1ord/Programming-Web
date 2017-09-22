using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.BusinessEntities
{
	public class BusinessEntitiesListViewModel : Base.ViewModelBase
	{
		public List<RequestForService.Models.BusinessEntities.BusinessEntity> List { get; set; }

		public System.Guid? ParentEntityId { get; set; }

		public System.Guid? ParentParentEntityId { get; set; }

		public string ParentParentEntityName { get; set; }
	}
}