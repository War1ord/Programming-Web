using BudgetManager.Models.Base;

namespace BudgetManager.Web.Base
{
    /// <summary>
    ///     The View Model Base
    /// </summary>
    public abstract class ViewModelBase
    {
        private Result _result;
        private string _returnUrl;

        public Result Result
        {
            get { return _result ?? (_result = new Result(string.Empty, ResultType.None)); }
            set { _result = value ?? new Result(string.Empty, ResultType.None); }
        }

        public string ReturnUrl
        {
            get { return _returnUrl ?? (_returnUrl = string.Empty); }
            set { _returnUrl = value ?? string.Empty; }
        }

        protected internal ViewModelBase SetResult(Business.Base.BusinessBase manager)
        {
            Result = manager.Result;
            return this;
        }

    }
}