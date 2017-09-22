using BudgetManager.Common.Messages;
using BudgetManager.Data;
using EntityFramework.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BudgetManager.Business.Error
{
	/// <summary>
	/// Log errors
	/// </summary>
	public static class ErrorManager
	{
		private delegate void LogExceptionCompletedEventHandler(Exception e);

		/// <summary>
		/// Log exceptions to database (this will be coding exceptions)
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <returns>A Friendly error text.</returns>
		public static string LogException(Exception exception)
		{
			LogExceptionCompletedEventHandler log = WriteToDb;
			log.BeginInvoke(exception, null, null);
			return CommonMessages.UnexpectedError;
		}

		public static List<Models.Error> GetList()
		{
			using (var db = new DataContext())
			{
				return db.Errors.OrderByDescending(i=>i.ErrorDate).ToList();
			}
		}

		#region Private Helpers

		/// <summary>
		/// Write the error to the database
		/// </summary>
		/// <param name="exception">The exception.</param>
		private static void WriteToDb(Exception exception)
		{
			using (var db = new DataContext())
			{
				var error = new Models.Error(exception);
				db.Errors.Add(error);
				db.SaveChanges();
			}
		}

		#endregion

		public static bool DeleteAll()
		{
			using (var db = new DataContext())
			{
				return db.Errors.Delete() > 0;
			}
		}
	}
}