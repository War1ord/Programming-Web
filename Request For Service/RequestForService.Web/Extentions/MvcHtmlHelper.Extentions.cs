using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using RequestForService.Business.Extensions;
using System.Dynamic;

namespace RequestForService.Web.Extentions
{
	public static class MvcHtmlHelperExtentions
	{
		public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
			Expression<Func<TModel, TProperty>> expression, string partialViewName)
		{
			var expressionText = ExpressionHelper.GetExpressionText(expression);
			var model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
			var viewData = new ViewDataDictionary(helper.ViewData)
			{
				Model = model,
				TemplateInfo = new TemplateInfo
				{
				    HtmlFieldPrefix = string.Format("{0}{1}",
				        !string.IsNullOrWhiteSpace(helper.ViewData.TemplateInfo.HtmlFieldPrefix)
				            ? helper.ViewData.TemplateInfo.HtmlFieldPrefix + "."
				            : string.Empty,
				        expressionText),
				},
			};
		    return helper.Partial(partialViewName, model, viewData);
		}

		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> html,
																		Expression<Func<TModel, TEnum>> expression,
																		string optionLabel = null,
																		object htmlAttributes = null)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
			Type enumType = GetNonNullableModelType(metadata);
			IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
			SelectList items = values.Select(v => new 
			{
				Value = v.ToString(),
				Text = v.ToDescription(),
			}).ToSelectList(i => i.Value, i => i.Text, metadata.Model);
			return html.DropDownListFor(expression, items, optionLabel, htmlAttributes);
		}

		public static SelectList ToSelectList<T, TProperty, TPropertyText>(this IEnumerable<T> list,
			Expression<Func<T, TProperty>> expressionForValueField,
			Expression<Func<T, TPropertyText>> expressionForTextField,
			TProperty selectedValue = default(TProperty))
		{
			if (list != null)
			{
				string valueFieldFullString = expressionForValueField.ToString();
				int indexOfForValueField = valueFieldFullString.IndexOf(".", StringComparison.Ordinal);
				var valueField = valueFieldFullString.Substring(indexOfForValueField >= 0 ? indexOfForValueField + 1 : 0);

				string textFieldFullString = expressionForTextField.ToString();
				int indexOfForTextField = textFieldFullString.IndexOf(".", StringComparison.Ordinal);
				var textField = textFieldFullString.Substring(indexOfForTextField >= 0 ? indexOfForTextField + 1 : 0);

				return new SelectList(list, valueField, textField, selectedValue);
			}
			else
			{
				return new SelectList(new List<string>());
			}
		}

	    public static MvcHtmlString DisplayWithBreaksFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
	        Expression<Func<TModel, TProperty>> expression, string separator = "")
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var seperateWith = string.IsNullOrWhiteSpace(separator) ? Environment.NewLine : separator;
			var model = htmlHelper.Encode(metaData.Model).Replace(seperateWith, "<br />" + seperateWith);
			if (string.IsNullOrWhiteSpace(model)) return MvcHtmlString.Empty;
			return MvcHtmlString.Create(model);
		}

        #region Helpers
		
		private static Type GetNonNullableModelType(ModelMetadata modelMetadata)
		{
			Type realModelType = modelMetadata.ModelType;

			Type underlyingType = Nullable.GetUnderlyingType(realModelType);
			if (underlyingType != null)
			{
				realModelType = underlyingType;
			}
			return realModelType;
		} 
		
		#endregion

	}

}