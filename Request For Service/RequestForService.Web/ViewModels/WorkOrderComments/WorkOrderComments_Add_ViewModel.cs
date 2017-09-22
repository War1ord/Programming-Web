using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using RequestForService.Models.Filters;
using RequestForService.Models.WorkOrders;

namespace RequestForService.Web.ViewModels.WorkOrderComments
{
    public class WorkOrderComments_Add_ViewModel : Base.ViewModelBase
    {
        public WorkOrder WorkOrder { get; set; }
        public WorkOrderComment Comment { get; set; }
    }
}