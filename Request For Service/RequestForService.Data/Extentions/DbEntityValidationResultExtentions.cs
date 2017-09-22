using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;

namespace RequestForService.Data.Extentions
{
	public static class DbEntityValidationResultExtentions
	{
		public static string ToHtmlValidMultiLineString(this ICollection<DbValidationError> errors)
		{
			try
			{
				var error = errors.Select(e => string.Format("{0}: {1}", e.PropertyName, e.ErrorMessage))
								.Aggregate((s1, s2) => string.Format("{0}<br/>{1}", s1, s2));
				return error;
			}
			catch
			{
				return null;
			}
		}
	}
}