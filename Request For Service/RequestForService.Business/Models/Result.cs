using System;
using RequestForService.Business.Extensions;

namespace RequestForService.Business.Models
{
    public enum ResultType
    {
        None = 0,
        Success = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        CriticalError = 50,
    }

    public abstract class ResultBase
    {
        public ResultBase(){}

        public ResultBase(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }

        public ResultBase(ResultType type, string message, Exception exception)
        {
            Type = type;
            Message = message;
            Exception = exception;
        }

        public ResultType Type { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public string TypeDescription
        {
            get { return Type.ToDescription(); }
        }

        public bool IsMessageSet
        {
            get { return !string.IsNullOrWhiteSpace(Message); }
        }
        public bool IsSuccessful
        {
            get { return Type == ResultType.Success; }
        }
        public bool IsInformation
        {
            get { return Type == ResultType.Information; }
        }
        public bool IsWarning
        {
            get { return Type == ResultType.Warning; }
        }
        public bool IsError
        {
            get { return Type == ResultType.Error; }
        }
        public bool IsCriticalError
        {
            get { return Type == ResultType.CriticalError; }
        }
        public bool IsNotSuccessful
        {
            get { return Type != ResultType.Success; }
        }
        public bool IsNotInformation
        {
            get { return Type != ResultType.Information; }
        }
        public bool IsNotWarning
        {
            get { return Type != ResultType.Warning; }
        }
        public bool IsNotError
        {
            get { return Type != ResultType.Error; }
        }
        public bool IsNotCriticalError
        {
            get { return Type != ResultType.CriticalError; }
        }
    }

	public class Result : ResultBase
	{
		public Result(ResultType type, string message) : base(type, message){}
		public Result(ResultType type, string message, Exception exception) : base(type, message, exception){}

		public Result<T> ToResult<T>(T entity)
		{
			return new Result<T>(Type, Message, entity);
		}
	}

	public class Result<T> : Result
	{
		public Result(ResultType type, string message, T entity) : base(type, message)
		{
			Entity = entity;
		}
		public Result(ResultType type, string message, T entity, Exception exception) : base(type, message, exception)
		{
			Entity = entity;
		}

		public T Entity { get; private set; }

		public bool IsValidEntity { get { return Entity != null; } }

		public Result ToResult()
		{
			return this;
		}

	}

	public static class Results
	{
		public static string SuccessMessage
		{
			get { return "Success"; }
		}
		public static string FailedMessage
		{
			get { return "An unexpected error occurred."; }
		}

		public static Result Result(ResultType type, string message)
		{
			return new Result(type, message);
		}
		public static Result<T> Result<T>(ResultType type, string message, T entity)
		{
			return new Result<T>(type, message, entity);
		}
		public static Result<T> Result<T>(ResultType type, T entity)
		{
			return new Result<T>(type, null, entity);
		}

		public static Result Result(ResultType type, string message, Exception exception)
		{
			return new Result(type, message, exception);
		}
		public static Result<T> Result<T>(ResultType type, string message, T entity, Exception exception)
		{
			return new Result<T>(type, message, entity, exception);
		}
		public static Result<T> Result<T>(ResultType type, string message, Exception exception)
		{
			return new Result<T>(type, message, default(T), exception);
		}

		public static Result SuccessResult()
		{
			return Result(ResultType.Success, SuccessMessage);
		}
		public static Result ErrorResult()
		{
			return Result(ResultType.Error, FailedMessage);
		}
		public static Result InvalidResult()
		{
			return Result(ResultType.Warning, "Invalid");
		}

		public static Result SuccessResult(string message)
		{
			return Result(ResultType.Success, message);
		}
		public static Result ErrorResult(string message)
		{
			return Result(ResultType.Error, message);
		}
		public static Result InvalidResult(string message)
		{
			return Result(ResultType.Warning, message);
		}

		public static Result<T> NoneResult<T>(T entity)
		{
			return Result(ResultType.None, entity);
		}
		public static Result<T> SuccessResult<T>(T entity)
		{
			return Result(ResultType.Success, SuccessMessage, entity);
		}
		public static Result<T> ErrorResult<T>(T entity)
		{
			return Result(ResultType.Error, FailedMessage, entity);
		}
		public static Result<T> InvalidResult<T>(T entity)
		{
			return Result(ResultType.Warning, "Invalid", entity);
		}

		public static Result<T> NoneResult<T>(string message, T entity)
		{
			return Result(ResultType.None, entity);
		}
		public static Result<T> SuccessResult<T>(string message, T entity)
		{
			return Result(ResultType.Success, message, entity);
		}
		public static Result<T> ErrorResult<T>(string message, T entity)
		{
			return Result(ResultType.Error, message, entity);
		}
		public static Result<T> InvalidResult<T>(string message, T entity)
		{
			return Result(ResultType.Warning, message, entity);
		}

		public static Result<T> ErrorResult<T>(string message)
		{
			return Result(ResultType.Error, message, default(T));
		}
		public static Result<T> ErrorResult<T>()
		{
			return Result(ResultType.Error, FailedMessage, default(T));
		}

	    public static Result ToResult(this Exception exception, string message)
	    {
            return Result(ResultType.Error, message, exception);
	    }
	    public static Result ToResult(this Exception exception)
	    {
            return Result(ResultType.Error, FailedMessage, exception);
	    }
	}
}