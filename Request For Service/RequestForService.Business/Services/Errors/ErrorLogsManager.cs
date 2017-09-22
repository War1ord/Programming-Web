using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using RequestForService.Business.Models;
using RequestForService.Models.Errors;
using RequestForService.Data;

namespace RequestForService.Business.Services.Errors
{
	public class ErrorLogsManager : Base.DataManagerBase
	{
		public ErrorLogsManager(Guid? userId) : base(userId) { }
		public ErrorLogsManager(DataContext db, Guid? userId) : base(db, userId) { }

		public Result<ErrorLog> GetEntity(Guid id, bool isDeleted = false)
		{
			var entity = Db.Set<ErrorLog>()
				.Include(i => i.CreatedByUser)
				.FirstOrDefault(i => i.Id == id && i.IsDeleted == isDeleted);
			return Results.SuccessResult(entity);
		}

		public Result<List<ErrorLog>> GetEntityList<TProperty>(Expression<Func<ErrorLog, TProperty>> sort,
			bool isSortDescending, bool isDeleted = false)
		{
			//return base.GetEntityList(sort, isSortDescending, isDeleted);
			try
			{
				if (isSortDescending)
				{
					var list = Db.Set<ErrorLog>().Include(i => i.CreatedByUser).OrderByDescending(sort).ToList();
					return Results.SuccessResult(list);
				}
				else
				{
					var list = Db.Set<ErrorLog>().Include(i => i.CreatedByUser).OrderBy(sort).ToList();
					return Results.SuccessResult(list);
				}
			}
			catch (Exception exception)
			{
				Logs.Log(exception, UserId);
				return Results.ErrorResult<List<ErrorLog>>();
			}
		}

		public Result DeleteAll()
		{
			try
			{
				Db.Database.ExecuteSqlCommand("truncate table ErrorLogs");
				return Results.SuccessResult("Deleted all Error Logs.");
			}
			catch (Exception exception)
			{
				Logs.Log(exception, UserId);
				return Results.ErrorResult("Failed deleting error logs.");
			}
		}
	}
}