using EntityFramework.Extensions;
using Report_Manager.WebSite.Business.Models;
using Report_Manager.WebSite.Data.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Report_Manager.WebSite.Business
{
	public class DatabaseConnectionsService : Base.BusinessBase
	{
		public DatabaseConnectionsService(User user, MethodBase callingMethod) : base(user, callingMethod) { }

		public Result Add(string name, string provider, string connectionString)
		{
			try
			{
				Db.DatabaseConnections.Add(new DatabaseConnection
				{
					CreatedBy = User,
					CreatedById = User.Id,
					DateTimeCreated = DateTime.Now,
					IsActive = true,
					Name = name,
					Provider = provider,
					ConnectionString = connectionString,
				});
				Db.SaveChanges();
				return new Result
				{
					Type = ResultType.Success,
					DisplayMessage = $@"Added new database connection.",
				};
			}
			catch (Exception e)
			{
				var result = Log(e);
				return result.Type == ResultType.Error ? result : new Result
				{
					Type = ResultType.Error,
					DisplayMessage = $@"Failed adding a database connection.",
					Exception = e,
				};
			}
		}

		public Result Update(int id, string name, string provider, string connectionString)
		{
			try
			{
				var changes = Db.DatabaseConnections
					.Where(i => i.Id == id)
					.Update(i => new DatabaseConnection
					{
						Name = name,
						Provider = provider,
						ConnectionString = connectionString,
					});
				return changes > 0
					? new Result(ResultType.Success, $@"Updated database connection.")
					: new Result(ResultType.Warning, $@"No changes when trying to update database connection.");
			}
			catch (Exception e)
			{
				var result = Log(e);
				return result.Type == ResultType.Error ? result : new Result
				{
					Type = ResultType.Error,
					DisplayMessage = $@"Failed updating database connection.",
					Exception = e,
				};
			}
		}

		public Result Delete(int id)
		{
			try
			{
				var changes = Db.DatabaseConnections
					.Where(i => i.Id == id)
					.Update(i => new DatabaseConnection
					{
						IsDeleted = true,
					});
				return changes > 0
					? new Result(ResultType.Success, $@"Deleted database connection.")
					: new Result(ResultType.Warning, $@"No changes when trying to delete database connection.");
			}
			catch (Exception e)
			{
				var result = Log(e);
				return result.Type == ResultType.Error ? result : new Result
				{
					Type = ResultType.Error,
					DisplayMessage = $@"Failed deleting database connection.",
					Exception = e,
				};
			}
		}
	}
}