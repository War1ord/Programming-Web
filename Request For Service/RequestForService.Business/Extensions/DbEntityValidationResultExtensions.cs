using RequestForService.Common.Extensions;
using System.Data.Entity.Validation;
using System.Linq;

namespace RequestForService.Business.Extensions
{
	public static class DbEntityValidationResultExtension
	{
		public static string ToMultilineString(this DbEntityValidationResult validationResult)
		{
			return validationResult.ValidationErrors.Select(i => i.ErrorMessage).Join();
		}
	}
}