using System;

namespace RequestForService.Business.Services.Errors
{
	public static class Logs
	{
		public static void Log(Exception exception, Guid? userId)
		{
			using (var errorLogs = new Services.Errors.ErrorLogService(exception, userId))
			{
				errorLogs.Save();
			}
		}
	}
}