using RequestForService.Business.Services.Errors;
using RequestForService.Data;
using System;

namespace RequestForService.Business.Base
{
	public class BusinessBase : IDisposable, Interfaces.IBusinessBase
	{
		private DataContext _db;
		protected DataContext Db
		{
			get { return _db ?? (_db = new DataContext()); }
		}

		protected Guid? UserId { get; private set; }
	    protected BusinessBase(Guid? userId)
	    {
	        UserId = userId;
	    }
	    protected BusinessBase(DataContext db, Guid? userId)
	    {
	        _db = db;
	        UserId = userId;
	    }

		internal void LogException(Exception exception)
		{
			using (var errorLogService = new ErrorLogService(exception, UserId))
			{
				var result = errorLogService.Save();
				//if not successful log to event viewer and to a website log file.
				if (!result.IsSuccessful)
				{
					//TODO: log to event viewer and to a website log file
				}
			}
		}

		public void Dispose()
		{
			if (_db != null)
			{
				_db.Dispose();
                _db = null;
            }
        }
	}
}