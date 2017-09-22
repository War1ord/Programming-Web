using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RequestForService.Models.ComplexTypes
{
	[ComplexType]
	public class JobDetails
	{
		[Display(Name = "Job Title")]
		public DataTypes.Enums.JobTitle? JobTitle { get; set; }
	}
}