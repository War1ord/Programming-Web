using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.Categories
{
	public class CategoriesListViewModel : Base.ViewModelBase
	{
		public List<RequestForService.Models.WorkOrders.Category> List { get; set; }
	}
}