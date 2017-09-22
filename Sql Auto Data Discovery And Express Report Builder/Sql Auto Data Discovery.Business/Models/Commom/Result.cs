using System;
using Sql_Auto_Data_Discovery.Business.Extentions;

namespace Sql_Auto_Data_Discovery.Business.Models.Commom
{
    public abstract class ResultBase
    {
        protected ResultBase() { }

        protected ResultBase(ResultType type, string message)
        {
            Type = type;
            Message = message;
        }

        public ResultType Type { get; set; }
        public string Message { get; set; }

        public bool IsMessageSet
        {
            get { return !Message.IsNullOrWhiteSpace(); }
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
        public Result(ResultType type, string message) : base(type, message) { }

        public Result<T> ToResult<T>(T entity)
        {
            return new Result<T>(Type, Message, entity);
        }
    }

    public class Result<T> : ResultBase
    {
        public Result(ResultType type, string message, T entity)
            : base(type, message)
        {
            Entity = entity;
        }

        public T Entity { get; private set; }

        public bool IsEntityValid { get { return IsTypeAbleToCheckForNull ? Entity != null : true; } }

        private bool IsTypeAbleToCheckForNull
        {
            get
            {
                var type = Entity.GetType();
                return type.IsClass
                       || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>));
            }
        }

        public Result ToResult()
        {
            return new Result(Type, Message);
        }
    }

    public static class Results
    {
        public static string SuccessMessage { get { return "Success"; } }
        public static string FailedMessage { get { return "An unexpected error occurred."; } }
        public static string InvalidMessage { get { return "Invalid"; } }

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

        public static Result NoneResult()
        {
            return Result(ResultType.None, SuccessMessage);
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
            return Result(ResultType.Warning, InvalidMessage);
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
            return Result(ResultType.Warning, InvalidMessage, entity);
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

        public static Result<T> InvalidResult<T>(string message)
        {
            return Result(ResultType.Warning, message, default(T));
        }

        public static Result<T> ErrorResult<T>()
        {
            return Result(ResultType.Error, FailedMessage, default(T));
        }
        public static Result<T> InvalidResult<T>()
        {
            return Result(ResultType.Warning, InvalidMessage, default(T));
        }

        public static Result<string> ToResultString(this string value)
        {
            return value.IsSet() ? SuccessResult(entity:value) : ErrorResult(entity:value);
        }
        public static Result<int?> ToResultInt(this int? value)
        {
            return value.IsSet() ? SuccessResult(value) : ErrorResult(value);
        }
        public static Result ToResult(this int rows_affected)
        {
            return rows_affected > 0 ? SuccessResult() : ErrorResult();
        }
        public static Result ToResult(this bool result)
        {
            return result ? SuccessResult() : ErrorResult();
        }
        public static Result<string> ToResult(this string result)
        {
            return result.IsSet() ? SuccessResult(entity: result) : ErrorResult(entity: result);
        }
        public static Result<int?> ToResultInt(this string result)
        {
            return result.IsSet() ? SuccessResult(entity: (int?)result.AsInt()) : ErrorResult(entity: (int?)null);
        }

        public static Result ToResult(this Exception exception)
        {
            return ErrorResult(string.Format("{0}{1}{2}", exception.Message, Environment.NewLine, exception.StackTrace));
        }

    }

    public enum ResultType
    {
        None = 0,
        Success = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        CriticalError = 50,
    }
}