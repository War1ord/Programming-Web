using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.Base
{
	public abstract class BusinessEntityCreatedByBase : CreatedByBase
	{
		[Required]
		[Display(Name = "Business Entity")]
		public Guid BusinessEntityId { get; set; }

		[ForeignKey("BusinessEntityId")]
		public BusinessEntities.BusinessEntity BusinessEntity { get; set; }
	}
}