using System;
using System.Collections.Generic;
using RequestForService.Business.Enums;
using RequestForService.Business.Models;
using RequestForService.Business.Services.WorkOrders;

namespace RequestForService.Web.ViewModels.WorkOrders
{
	public class WorkOrder_List_ViewModel : Base.ViewModelBase
	{
		#region Fields
		private DateTime _fromDate;
		private DateTime _toDate;

		#endregion

        public List<RequestForService.Models.WorkOrders.WorkOrder> List { get; set; }
		public string SearchText { get; set; }
		public DateTime FromDate
		{
			get
			{
				return _fromDate == DateTime.MinValue ? (_fromDate = DateTime.Today.AddMonths(-1)) : _fromDate;
			}
			set { _fromDate = value; }
		}
		public DateTime ToDate
		{
			get
			{
				return _toDate == DateTime.MinValue ? (_toDate = DateTime.Today.Add(new TimeSpan(TimeSpan.TicksPerDay))) : _toDate;
			}
			set { _toDate = value; }
		}

		public WorkOrderSortBy SortBy { get; set; }

		public List<EntityDisplay> BusinessEntityList { get; set; }
		public List<EntityDisplay> UsersList { get; set; }

		public Guid? SelectedBusinessEntityId { get; set; }
		public Guid? SelectedUserId { get; set; }

		public WorkOrderSummaryParams Parameters
		{
			get
			{
				return new WorkOrderSummaryParams
				{
					SearchText = SearchText,
					FromDate = FromDate,
					ToDate = ToDate,
					SelectedBusinessEntityId = SelectedBusinessEntityId,
					SelectedUserId = SelectedUserId,
					SortBy = SortBy,
				};
			}
		}
	}
}