using System;

namespace Report_Manager.WebSite.Business.Models
{
	public enum ResultType
	{
		Success = 1,
		Warning,
		Failed,
		Error,
	}

	public class Result
	{
		public Result() { }
		public Result(ResultType type, string displayMessage) { Type = type; DisplayMessage = displayMessage; }
		public Result(ResultType type, string displayMessage, Exception exception) { Type = type; DisplayMessage = displayMessage; Exception = exception; }
		public ResultType Type { get; set; }
		public string DisplayMessage { get; set; }
		public Exception Exception { get; set; }

		public ReturnResult<T> ToResult<T>()
		{
			return new ReturnResult<T>
			{
				Type = Type,
				DisplayMessage = DisplayMessage,
				Exception = Exception,
			};
		}
		public ReturnResult<T> ToResult<T>(T data)
		{
			return new ReturnResult<T>
			{
				Type = Type,
				DisplayMessage = DisplayMessage,
				Exception = Exception,
				Data = data,
			};
		}
	}

	public class ReturnResult<T> : Result
	{
		public ReturnResult() { }
		public ReturnResult(ResultType type, string displayMessage, T data) : base(type, displayMessage) { Data = data; }
		public ReturnResult(ResultType type, string displayMessage, Exception exception) : base(type, displayMessage, exception) { }
		public T Data { get; set; }
	}
}