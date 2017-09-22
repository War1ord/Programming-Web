using RequestForService.Business.Models;

namespace RequestForService.Web.ViewModels.WorkOrders
{
    public class WorkOrder_Details_ViewModel : Base.ViewModelBase
    {
        public WorkOrder_Details_ViewModel()
        {
            WorkOrder = new Result<RequestForService.Models.WorkOrders.WorkOrder>(ResultType.None, null, null);
        }

        public Result<RequestForService.Models.WorkOrders.WorkOrder> WorkOrder { get; set; }
    }
}