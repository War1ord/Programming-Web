namespace RequestForService.Business.Services.WorkOrders
{
    public struct WorkOrderSummaryParams
    {
        public WorkOrderSummaryParams(string searchText, System.DateTime fromDate, System.DateTime toDate, System.Guid? selectedBusinessEntityId, System.Guid? selectedUserId, Enums.WorkOrderSortBy sortBy) : this()
        {
            SearchText = searchText;
            FromDate = fromDate;
            ToDate = toDate;
            SelectedBusinessEntityId = selectedBusinessEntityId;
            SelectedUserId = selectedUserId;
            SortBy = sortBy;
        }

        public string SearchText { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public System.Guid? SelectedBusinessEntityId { get; set; }
        public System.Guid? SelectedUserId { get; set; }
        public Enums.WorkOrderSortBy SortBy { get; set; }
    }
}