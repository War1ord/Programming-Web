namespace BudgetManager.Models.Base
{
    public class Result
    {
        private string _message;
        public Result(string message, ResultType resultType)
        {
            Set(message, resultType);
        }
        public Result()
        {
            Message = string.Empty;
            Type = ResultType.None;
        }
        public void Set(string message, ResultType resultType)
        {
            Message = message;
            Type = resultType;
        }
        public ResultType Type { get; set; }
        public string Message
        {
            get { return _message ?? (_message = string.Empty); }
            set { _message = value ?? string.Empty; }
        }
    }
}