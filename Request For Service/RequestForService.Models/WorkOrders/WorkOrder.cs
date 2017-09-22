using RequestForService.DataTypes.Enums;
using RequestForService.Models.List;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrder : Base.CreatedByBase
	{
        #region fields
        private List<WorkOrderComment> comments;
        private List<WorkItem> workItems;
        private List<WorkOrderAttachment> attachments;
        private List<WorkOrderNote> notes;
        private List<Estimation> estimations;
        private List<Invoice> invoices;
        private KeyList<Guid, WorkItem> _workItemsKeyList;
        private KeyList<Guid, WorkOrderAttachment> _workOrderAttachmentsKeyList;
        private KeyList<Guid, WorkOrderNote> _workOrderNotesKeyList;
        private KeyList<Guid, Estimation> _estimationsKeyList;
        private KeyList<Guid, WorkOrderComment> _workOrderCommentsKeyList;
        private KeyList<Guid, Invoice> _invoicesKeyList; 
        #endregion

	    [Index(IsUnique = true), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Display(Name = "Work Order")]
		public long WorkOrderId { get; set; }
		[Display(Name = "Reference")]
		public string Reference { get; set; }
		[Required, StringLength(250)]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public Priority Priority { get; set; }
		public WorkOrderStatus Status { get; set; }

		[Display(Name = "Assigned To User")]
		public Guid? AssignedToUserId { get; set; }
		[Display(Name = "Type")]
		public Guid WorkOrderTypeId { get; set; }
		[Display(Name = "Hourly Rate")]
		public Guid? HourlyRateId { get; set; }
        [Display(Name = "Category")]
        public Guid? CategoryId { get; set; }

		[ForeignKey("AssignedToUserId")]
		public Users.User AssignedToUser { get; set; }
		[ForeignKey("WorkOrderTypeId")]
		public WorkOrderType WorkOrderType { get; set; }
		[ForeignKey("HourlyRateId")]
		public HourlyRate HourlyRate { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

		public List<WorkItem> WorkItems
		{
			get { return workItems ?? (workItems = new List<WorkItem>()); }
			set { workItems = value; }
		}
		public List<WorkOrderAttachment> Attachments
		{
			get { return attachments ?? (attachments = new List<WorkOrderAttachment>()); }
			set { attachments = value; }
		}
		public List<WorkOrderNote> Notes
		{
			get { return notes ?? (notes = new List<WorkOrderNote>()); }
			set { notes = value; }
		}
		public List<Estimation> Estimations
		{
			get { return estimations ?? (estimations = new List<Estimation>()); }
			set { estimations = value; }
		}
		public List<WorkOrderComment> Comments
		{
			get { return comments ?? (comments = new List<WorkOrderComment>()); }
			set { comments = value; }
		}
		public List<Invoice> Invoices
		{
			get { return invoices ?? (invoices = new List<Invoice>()); }
			set { invoices = value; }
		}

		[NotMapped]
		public bool IsViewed { get { return Status == WorkOrderStatus.Assigned; } }
		[NotMapped]
		public bool IsCompleted { get { return Status == WorkOrderStatus.Completed; } }
		[NotMapped]
		public bool IsSignedOff { get { return Status == WorkOrderStatus.SignedOff; } }

	    [NotMapped]
	    public KeyList<Guid, WorkItem> WorkItemsKeyList
	    {
            get { return _workItemsKeyList ?? (_workItemsKeyList = new KeyList<Guid, WorkItem>(Id, WorkItems)); }
	        set { _workItemsKeyList = value; }
	    }
        [NotMapped]
        public KeyList<Guid, WorkOrderAttachment> WorkOrderAttachmentsKeyList
	    {
	        get { return _workOrderAttachmentsKeyList ?? (_workOrderAttachmentsKeyList = new KeyList<Guid, WorkOrderAttachment>(Id, Attachments)); }
	        set { _workOrderAttachmentsKeyList = value; }
	    }
	    [NotMapped]
	    public KeyList<Guid, WorkOrderNote> WorkOrderNotesKeyList
	    {
	        get { return _workOrderNotesKeyList ?? (_workOrderNotesKeyList = new KeyList<Guid, WorkOrderNote>(Id, Notes)); }
	        set { _workOrderNotesKeyList = value; }
	    }
	    [NotMapped]
	    public KeyList<Guid, Estimation> EstimationsKeyList
	    {
	        get { return _estimationsKeyList ?? (_estimationsKeyList = new KeyList<Guid, Estimation>(Id, Estimations)); }
	        set { _estimationsKeyList = value; }
	    }
	    [NotMapped]
	    public KeyList<Guid, WorkOrderComment> WorkOrderCommentsKeyList
	    {
	        get { return _workOrderCommentsKeyList ?? (_workOrderCommentsKeyList = new KeyList<Guid, WorkOrderComment>(Id, Comments)); }
	        set { _workOrderCommentsKeyList = value; }
	    }
	    [NotMapped]
	    public KeyList<Guid, Invoice> InvoicesKeyList
	    {
	        get { return _invoicesKeyList ?? (_invoicesKeyList = new KeyList<Guid, Invoice>(Id, Invoices)); }
	        set { _invoicesKeyList = value; }
	    }
	}
}