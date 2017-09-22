using System.Collections.Generic;

namespace RequestForService.Web.ViewModels.HourlyRates
{
	public class HourlyRatesListViewModel : Base.ViewModelBase
	{
		public List<RequestForService.Models.WorkOrders.HourlyRate> List { get; set; }
	}
}