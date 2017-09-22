using System.ComponentModel.DataAnnotations;
using RequestForService.Models.Base;

namespace RequestForService.Models.WorkOrders
{
	public class WorkOrderAttachment : WorkOrderCreatedByBase
	{
		[Required]
		public byte[] Bytes { get; set; }
		[Required]
		public string FileName { get; set; }
		[Required]
		public string ContentType { get; set; }
		[Required]
		public int ContentLength { get; set; }
	}
}