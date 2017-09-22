using Project_Manager.Data.Model.Finance;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Project
{
	public class ProjectInvoiceLink : Base.ProjectBase
	{
		[Required]
		public Guid InvoiceId { get; set; }

		[ForeignKey(nameof(InvoiceId))]
		public virtual Invoice Invoice { get; set; }

	}
}