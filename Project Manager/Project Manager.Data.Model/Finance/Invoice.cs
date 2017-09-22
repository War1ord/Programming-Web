using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Finance
{
	public class Invoice : Base.ModelBase
	{
		[Required]
		public Guid AccountId { get; set; }

		public virtual List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();

		[ForeignKey(nameof(AccountId))]
		public virtual Account Account { get; set; }

	}
}