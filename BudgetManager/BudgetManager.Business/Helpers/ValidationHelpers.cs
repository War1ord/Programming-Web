using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Text;

namespace BudgetManager.Business.Helpers
{
    public static class ValidationHelpers
    {
        public static string GetValidationErrorMessage(string format, ICollection<DbValidationError> errors)
        {
            StringBuilder s = new StringBuilder();
            foreach (var error in errors)
            {
                s.Append(string.Format("{0}\r\n", error.ErrorMessage));
            }
            return string.Format(format, s);
        }
    }
}