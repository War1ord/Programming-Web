using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Manager.Data.Model.Finance
{
	public class InvoiceItem : Base.ModelBase
	{
		[Required]
		public Guid InvoiceId { get; set; }

		[Required]
		public string Description { get; set; }
		[Required]
		public decimal AmountExclVat { get; set; }
		[Required]
		public decimal Vat { get; set; }

		[ForeignKey(nameof(InvoiceId))]
		public virtual Invoice Invoice { get; set; }
	}
}