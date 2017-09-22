using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace BudgetManager.Extentions.Mvc
{
	public static class SelectListExtentions
	{
		/// <summary>
		/// To select list helper
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty1">The type of the property1.</typeparam>
		/// <typeparam name="TProperty2">The type of the property2.</typeparam>
		/// <param name="list">The list.</param>
		/// <param name="expressionForValueField">The expression for value field.</param>
		/// <param name="expressionForTextField">The expression for text field.</param>
		/// <returns></returns>
		public static SelectList ToSelectList<T, TProperty1, TProperty2>(this IEnumerable<T> list, Expression<Func<T, TProperty1>> expressionForValueField, Expression<Func<T, TProperty2>> expressionForTextField) where T : new()
		{
			if (list == null) return new SelectList(new[] {"0", ""});
			return list.ToSelectList(expressionForValueField, expressionForTextField, default(TProperty1));
		}

		/// <summary>
		/// To select list helper with selected value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <typeparam name="TProperty1">The type of the property1.</typeparam>
		/// <typeparam name="TProperty2">The type of the property2.</typeparam>
		/// <param name="list">The list.</param>
		/// <param name="expressionForValueField">The expression for value field.</param>
		/// <param name="expressionForTextField">The expression for text field.</param>
		/// <param name="selectedValue">The selected value.</param>
		/// <returns></returns>
		public static SelectList ToSelectList<T, TProperty1, TProperty2>(this IEnumerable<T> list, Expression<Func<T, TProperty1>> expressionForValueField, Expression<Func<T, TProperty2>> expressionForTextField, TProperty1 selectedValue) where T : new()
		{
			if (list == null) return new SelectList(new[] {"0", ""});
			
			string valueFieldFullString = expressionForValueField.ToString();
			int indexOfForValueField = valueFieldFullString.IndexOf(".", StringComparison.Ordinal);
			var valueField = valueFieldFullString.Substring(indexOfForValueField >= 0 ? indexOfForValueField + 1 : 0);
			string textFieldFullString = expressionForTextField.ToString();
			int indexOfForTextField = textFieldFullString.IndexOf(".", StringComparison.Ordinal);
			var textField = textFieldFullString.Substring(indexOfForTextField >= 0 ? indexOfForTextField + 1 : 0);
			return new SelectList(list, valueField, textField, selectedValue);
		}

		/// <summary>
		/// Inserts test to the top of the list.
		/// </summary>
		/// <param name="selectList">The select list.</param>
		/// <param name="text">The text.</param>
		/// <returns></returns>
		public static SelectList Insert(this SelectList selectList, string text = "")
		{
			if (selectList != null)
			{
				List<SelectListItem> list = selectList.ToList();
				list.Insert(0, new SelectListItem { Value = "0", Text = text, Selected = true });
				return new SelectList(list, "value", "text", "0");
			}
			return new SelectList(new[] { "0", "" });
		}

	}
}