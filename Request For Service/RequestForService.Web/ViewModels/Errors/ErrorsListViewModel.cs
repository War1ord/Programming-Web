using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.Errors
{
	public class ErrorsListViewModel : Base.ViewModelBase
	{
		public List<RequestForService.Models.Errors.ErrorLog> List { get; set; }
	}
}