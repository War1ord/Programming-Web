using System;
using RequestForService.Business.Models;

namespace RequestForService.Business.Services.Errors
{
	internal class ErrorLogService : Base.DataManagerBase
	{
		private Exception Exception { get; set; }

		public ErrorLogService(Exception exception, Guid? userId) : base(userId)
		{
			Exception = exception;
		}

		public Result Save()
		{
			try
			{
				var baseException = Exception.GetBaseException();
				var isTargetSiteValid = baseException.TargetSite != null;
				var isDeclaringTypeValid = isTargetSiteValid && baseException.TargetSite.DeclaringType != null;
				using (Db)
				{
					var errorLog = new RequestForService.Models.Errors.ErrorLog
					{
						Host = Environment.MachineName,
						Version = Environment.Version.ToString(),
						StackTrace = baseException.StackTrace ?? string.Empty,
						Message = Exception.Message != baseException.Message
									? baseException.Message + " (" + Exception.Message + ")"
									: Exception.Message,
						Method = isTargetSiteValid
									? baseException.TargetSite.Name
									: string.Empty,
						Class = isDeclaringTypeValid
									? baseException.TargetSite.DeclaringType.Name
									: string.Empty,
						Namespace = isDeclaringTypeValid
									? baseException.TargetSite.DeclaringType.Namespace
									: string.Empty,
						CreatedByUserId = UserId,
					};
					Add(errorLog);
					Db.SaveChanges();
				}
				return Results.SuccessResult();
			}
			catch (Exception e)
			{
				return Results.ErrorResult(e.Message);
			}
		}
	}
}