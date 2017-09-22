using Report_Manager.WebSite.Business.Models;
using Report_Manager.WebSite.Data.Models;
using System;
using System.Reflection;

namespace Report_Manager.WebSite.Business.Base
{
	public abstract class BusinessBase : IDisposable
	{
		private Data.DataContext db;
		protected Data.DataContext Db { get { return db ?? (db = Data.DataContext.I); } }

		protected User User;
		protected MethodBase CallingMethod;

		public BusinessBase(User user, MethodBase callingMethod)
		{
			User = user;
			CallingMethod = callingMethod;
		}

		public Result Log(Exception exception)
		{
			try
			{
				var now = DateTime.Now;
				Db.Errors.Add(new Error
				{
					CreatedById = User.Id,
					CreatedBy = User,
					DateTimeCreated = now,
					HelpLink = exception.HelpLink,
					HResult = exception.HResult,
					Message = exception.Message,
					Source = exception.Source,
					StackTrace = exception.StackTrace,
					IsActive = true,
					InnerException = GetInnerError(exception.InnerException),
				});
				Db.SaveChanges();
				return new Result
				{
					Type = ResultType.Success,
					DisplayMessage = "Exception logged successfully",
				};
			}
			catch (Exception e)
			{
				return new Result
				{
					Type = ResultType.Error,
					DisplayMessage = $@"Unexpected error while logging error",
					Exception = e,
				};
			}
		}

		private Error GetInnerError(Exception innerException)
		{
			if (innerException != null)
			{
				return new Error
				{
					CreatedById = User.Id,
					CreatedBy = User,
					DateTimeCreated = DateTime.Now,
					HelpLink = innerException.HelpLink,
					HResult = innerException.HResult,
					Message = innerException.Message,
					Source = innerException.Source,
					StackTrace = innerException.StackTrace,
					IsActive = true,
					InnerException = GetInnerError(innerException.InnerException),
				};
			}
			else
			{
				return null;
			}
		}

		public void Dispose()
		{
			db?.Dispose();
			db = null;
		}
	}

}
