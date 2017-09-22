using BudgetManager.Business.Error;
using BudgetManager.Common;
using BudgetManager.Common.Messages;
using BudgetManager.Data;
using BudgetManager.Extentions;
using BudgetManager.Models.Base;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;

namespace BudgetManager.Business.Base
{
	/// <summary>
	/// The Base Class Containing the DataContext connection and related content to all business classes. 
	/// </summary>
	public abstract class BusinessBase : IDisposable
	{
		protected readonly DataContext Db = new DataContext();
		private Result _result;

		protected BusinessBase()
		{
			if (_result == null)
			{
				_result = new Result(string.Empty, ResultType.Success);
			}
		}

		public Exception Exception { get; set; }
		public Result Result
		{
			get { return _result ?? (_result = new Result(string.Empty, ResultType.Success)); }
			set { _result = value ?? new Result(string.Empty, ResultType.Success); }
		}

		#region Bulk insert (use for performance data inserts)

		/// <summary>
		/// Function Wrapper to use the Sql Bulk Copy Class to insert a Data Table to 
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		public bool InsertBulk(DataTable dataTable)
		{
			return DataManagement.SqlBulkCopyToDatabase(dataTable, Db.Database.Connection.ConnectionString);
		}

		#endregion

		#region Validation

		public ValidationResult GetValidationResult<T>(T entity) where T : class
		{
			var dictionary = new ValidationResult();
			var result = Db.Entry(entity).GetValidationResult();
			result.ValidationErrors.ForEach(i => dictionary.ValidationErrors.Add(i.PropertyName, i.ErrorMessage));
			dictionary.IsValid = result.IsValid;
			return dictionary;
		}

		#endregion

		#region Convert Helpers

		protected string ToMultilineString(ValidationResult validationResult)
		{
			return validationResult.ValidationErrors.Select(i => i.Value).Concatenate();
		}

		protected string ToMultilineString(IEnumerable<string> strings)
		{
			return strings.Concatenate();
		}

		#endregion

		#region On Exception

		public bool LogException(Exception e)
		{
			ErrorManager.LogException(e);
			Result.Message = CommonMessages.UnexpectedError;
			return false;
		}

		#endregion

		#region Business Class Helpers

		public BusinessBase Set<T, TProp>(Expression<Func<T, TProp>> expression, TProp value)
		{
			return this;
		}

		#endregion

		#region Implementation of IDisposable

		/// <summary>
		/// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
		/// </summary>
		public void Dispose()
		{
			Db.Dispose();
		}

		#endregion
	}
}