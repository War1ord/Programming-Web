using Report_Manager.WebSite.Business.Models;
using Report_Manager.WebSite.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Report_Manager.WebSite.Business
{
	public class ReportsService : Base.BusinessBase
	{
		public ReportsService(User user, MethodBase callingMethod) : base(user, callingMethod) { }

		public ReturnResult<List<Report>> GetReports(ReportFilter filter)
		{
			try
			{
				var q = (
					from i in Db.Reports
					where filter.
				);
				return q.;
			}
			catch (Exception e)
			{
				var result = Log(e);
				return result.Type == ResultType.Error
					? result.ToResult(new List<Report>())
					: new ReturnResult<List<Report>>(ResultType.Error, $@"Unexpected error.", e);
			}
		}
	}
}