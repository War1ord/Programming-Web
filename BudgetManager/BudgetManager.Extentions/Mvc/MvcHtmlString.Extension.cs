using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using NHibernate.Linq;

namespace BudgetManager.Extentions.Mvc
{
	/// <summary>
	/// A static class to contain the Enum DropDown Extensions
	/// </summary>
	public static class MvcHtmlStringHelperExtensions
	{
		#region EnumDropDownListFor

		/// <summary>
		/// Helper for enum to DropDownList.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="name">The name.</param>
		/// <param name="selectedValue">The selected value.</param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownList<TEnum>(this HtmlHelper htmlHelper, string name, TEnum selectedValue)
		{
			IEnumerable<TEnum> values = Enum.GetValues(typeof (TEnum))
				.Cast<TEnum>();
			IEnumerable<SelectListItem> items =
				from value in values
				select new SelectListItem
				       {
					       Text = value.ToString(),
					       Value = value.ToString(),
					       Selected = (value.Equals(selectedValue))
				       };
			return htmlHelper.DropDownList(
				name,
				items
				);
		}

		/// <summary>
		/// Helper for enum to DropDownList.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
		{
			var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			return !String.IsNullOrWhiteSpace(metadata.Watermark)
                ? htmlHelper.EnumDropDownListFor(expression, metadata.Watermark)
                : htmlHelper.EnumDropDownListFor(expression, null, null);
		}

		/// <summary>
		/// Helper for enum to DropDownList.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="optionLabel">The option label.</param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
		                                                               Expression<Func<TModel, TEnum>> expression,
		                                                               string optionLabel)
		{
			MvcHtmlString htmlString;
			if (!String.IsNullOrWhiteSpace(optionLabel))
			{
				htmlString = EnumDropDownListFor(htmlHelper, expression, optionLabel, null);
			}
			else
			{
				htmlString = EnumDropDownListFor(htmlHelper, expression, String.Empty, null);
			}
			return htmlString;
		}

		/// <summary>
		/// Helper for enum to DropDownList.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="optionLabel">The Option Label</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
		                                                               Expression<Func<TModel, TEnum>> expression,
		                                                               string optionLabel, object htmlAttributes)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			Type enumType = GetNonNullableModelType(metadata);
			IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
			IEnumerable<SelectListItem> items = values.Select(value => new SelectListItem
			{
				Text = EnumExtension.GetEnumDescription(value),
				Value = value.ToString(),
				Selected = value.Equals(metadata.Model)
			}).ToList();
			// If the enum is nullable, add an 'empty' item to the collection
			if (metadata.IsNullableValueType)
			{
				items.ForEach(i=>i.Selected = false);
				items = SingleEmptyItem.Concat(items);
			}
			return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
		}

		/// <summary>
		/// Helper for enum to DropDownList.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper,
		                                                               Expression<Func<TModel, TEnum>> expression,
		                                                               Dictionary<string, object> htmlAttributes)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			Type enumType = GetNonNullableModelType(metadata);
			IEnumerable<TEnum> values = Enum.GetValues(enumType).Cast<TEnum>();
			IEnumerable<SelectListItem> items = from value in values
			                                    select new SelectListItem
			                                           {
				                                           Text = EnumExtension.GetEnumDescription(value),
				                                           Value = value.ToString(),
				                                           Selected = value.Equals(metadata.Model)
			                                           };
			// If the enum is nullable, add an 'empty' item to the collection
			if (metadata.IsNullableValueType)
			{
				items = SingleEmptyItem.Concat(items);
			}
			return htmlHelper.DropDownListFor(expression, items, htmlAttributes);
		}

		#endregion

		#region CheckBoxListFor

		/// <summary>
		/// Helper for CheckBoxList.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="multiSelectList">The multi select list.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString CheckBoxListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
		                                                               Expression<Func<TModel, TProperty[]>> expression,
		                                                               MultiSelectList multiSelectList,
		                                                               object htmlAttributes = null)
		{
			//Derive property name for checkbox name
			MemberExpression body = expression.Body as MemberExpression;
			string propertyName = body.Member.Name;
			//Get currently select values from the ViewData model
			TProperty[] list = expression.Compile().Invoke(htmlHelper.ViewData.Model);
			//Convert selected value list to a List<string> for easy manipulation
			List<string> selectedValues = new List<string>();
			if (list != null)
			{
				selectedValues = new List<TProperty>(list).ConvertAll<string>(delegate(TProperty i) { return i.ToString(); });
			}
			//Create div
			TagBuilder divTag = new TagBuilder("div");
			divTag.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
			//Add checkboxes
			foreach (SelectListItem item in multiSelectList)
			{
				divTag.InnerHtml += String.Format("<div><input type=\"checkbox\" name=\"{0}\" id=\"{0}_{1}\" " +
				                                  "value=\"{1}\" {2} /><label for=\"{0}_{1}\">{3}</label></div>",
				                                  propertyName,
				                                  item.Value,
				                                  selectedValues.Contains(item.Value) ? "checked=\"checked\"" : "",
				                                  item.Text);
			}
			return MvcHtmlString.Create(divTag.ToString());
		}

		public static IHtmlString CheckboxListForEnum<T>(this HtmlHelper html, string name, T modelItems) where T : struct
		{
			StringBuilder sb = new StringBuilder();
			foreach (T item in Enum.GetValues(typeof (T)).Cast<T>())
			{
				TagBuilder builder = new TagBuilder("input");
				long targetValue = Convert.ToInt64(item);
				long flagValue = Convert.ToInt64(modelItems);
				if ((targetValue & flagValue) == targetValue)
				{
					builder.MergeAttribute("checked", "checked");
				}
				builder.MergeAttribute("type", "checkbox");
				builder.MergeAttribute("value", item.ToString());
				builder.MergeAttribute("name", name);
				builder.InnerHtml = item.ToString();
				sb.Append(builder.ToString(TagRenderMode.Normal));
			}
			return new HtmlString(sb.ToString());
		}

		#endregion

		#region PartialFor

		/// <summary>
		/// Helper for PartialFor.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="helper">The helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="partialViewName">Partial name of the view.</param>
		/// <returns></returns>
		public static MvcHtmlString PartialFor<TModel, TProperty>(this HtmlHelper<TModel> helper,
		                                                          Expression<Func<TModel, TProperty>> expression,
		                                                          string partialViewName)
		{
			//NOTE: for partial views having a list model this may give some problems, have the loop outside the partial view. 
			string name = ExpressionHelper.GetExpressionText(expression);
			object model = ModelMetadata.FromLambdaExpression(expression, helper.ViewData).Model;
			var viewData = new ViewDataDictionary(helper.ViewData)
			               {
				               Model = model,
				               TemplateInfo = new TemplateInfo
				                              {
					                              HtmlFieldPrefix =
						                              String.Format("{0}{1}",
						                                            !String.IsNullOrWhiteSpace(
							                                            helper.ViewData.TemplateInfo.HtmlFieldPrefix)
							                                            ? helper.ViewData.TemplateInfo.HtmlFieldPrefix + "."
							                                            : String.Empty,
						                                            name
						                              )
				                              }
			               };
			return helper.Partial(partialViewName, model, viewData);
		}

		#endregion

		#region RadioButtonFor

		/// <summary>
		/// Radio button for select list.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="listOfValues">The list of values.</param>
		/// <returns></returns>
		public static MvcHtmlString RadioButtonForSelectList<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
		                                                                        Expression<Func<TModel, TProperty>> expression,
		                                                                        IEnumerable<SelectListItem> listOfValues)
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var sb = new StringBuilder();
			if (listOfValues != null)
			{
				// Create a radio button for each item in the list 
				foreach (SelectListItem item in listOfValues)
				{
					// Generate an id to be given to the radio button field 
					var id = String.Format("{0}_{1}", metaData.PropertyName, item.Value);
					// Create and populate a radio button using the existing html helpers 
					var label = htmlHelper.Label(id, HttpUtility.HtmlEncode(item.Text));
					var radio = htmlHelper.RadioButtonFor(expression, item.Value, new {id = id}).ToHtmlString();
					// Create the html string that will be returned to the client 
					// e.g. <input data-val="true" data-val-required=
					//   "You must select an option" id="TestRadio_1" 
					//   name="TestRadio" type="radio" 
					//   value="1" /><label for="TestRadio_1">Line1</label> 
					sb.AppendFormat("<div class=\"RadioButton\">{0}{1}</div>", radio, label);
				}
			}
			return MvcHtmlString.Create(sb.ToString());
		}

		/// <summary>
		/// Radio button for enum.
		/// </summary>
		/// <typeparam name="TModel">The type of the model.</typeparam>
		/// <typeparam name="TProperty">The type of the property.</typeparam>
		/// <param name="htmlHelper">The HTML helper.</param>
		/// <param name="expression">The expression.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
		                                                                  Expression<Func<TModel, TProperty>> expression,
		                                                                  object htmlAttributes = null)
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var names = Enum.GetNames(metaData.ModelType);
			var sb = new StringBuilder();
			foreach (var name in names)
			{
				var id = String.Empty;
				if (!String.IsNullOrWhiteSpace(htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix))
				{
					id = String.Format(
						"{0}_{1}_{2}",
						htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
						metaData.PropertyName,
						name
						);
					//id = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(id);
				}
				else
				{
					id = String.Format(
						"{0}_{1}",
						metaData.PropertyName,
						name
						);
					//id = htmlHelper.ViewData.TemplateInfo.GetFullHtmlFieldId(id);
				}
				var radio = htmlHelper.RadioButtonFor(expression, name, new {id}).ToHtmlString();
				sb.AppendFormat(
					"<label for=\"{0}\">{1}</label> {2}",
					id,
					HttpUtility.HtmlEncode(name),
					radio
					);
			}
			return MvcHtmlString.Create(sb.ToString());
		}

		#endregion

		#region Private Helpers

		/// <summary>
		/// Gets the type of the non nullable model.
		/// </summary>
		/// <param name="modelMetadata">The model metadata.</param>
		/// <returns></returns>
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

		/// <summary>
		/// The single empty item
		/// </summary>
		private static readonly SelectListItem[] SingleEmptyItem = new[] {new SelectListItem {Text = "", Value = "",Selected = true}};

		#endregion

		#region Pager

		/// <summary>
		/// Pager for specified helper.
		/// </summary>
		/// <param name="helper">The helper.</param>
		/// <param name="currentPage">The current page.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="totalItemCount">The total item count.</param>
		/// <param name="routeValues">The route values.</param>
		/// <param name="htmlAttributes">The HTML attributes.</param>
		/// <returns></returns>
		public static MvcHtmlString Pager(this HtmlHelper helper, int currentPage, int pageSize, int totalItemCount, object routeValues = null, object htmlAttributes = null)
		{
			// how many pages to display in each page group const  
			int cGroupSize = 5;
			var pageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize);

			// cleanup any out bounds page number passed   
			currentPage = Math.Max(currentPage, 1);
			currentPage = Math.Min(currentPage, pageCount);

			var urlHelper = new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection);
			var container = new TagBuilder("div");
			if (htmlAttributes != null)
			{
				var properties = htmlAttributes.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var property in properties)
				{
					container.Attributes.Add(property.Name, property.GetValue(htmlAttributes, null).ToString());
				}
			}
			container.AddCssClass("pager");
			var actionName = helper.ViewContext.RouteData.GetRequiredString("Action");

			// calculate the last page group number starting from the current page     
			// until we hit the next whole divisible number    
			var lastGroupNumber = currentPage;
			while ((lastGroupNumber % cGroupSize != 0)) lastGroupNumber++;

			// correct if we went over the number of pages     
			var groupEnd = Math.Min(lastGroupNumber, pageCount);

			// work out the first page group number, we use the lastGroupNumber instead of     
			// groupEnd so that we don't include numbers from the previous group if we went    
			// over the page count     
			var groupStart = lastGroupNumber - (cGroupSize - 1);

			// if we are past the first page   
			if (currentPage > 1)
			{
				var previous = new TagBuilder("a");
				previous.SetInnerText("<");
				previous.AddCssClass("previous");
				var routingValues = new RouteValueDictionary(routeValues) {{"page", currentPage - 1}, {"pageSize", pageSize}};
				previous.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
				container.InnerHtml += previous.ToString();
			}

			// if we have past the first page group    
			if (currentPage > cGroupSize)
			{
				var previousDots = new TagBuilder("a");
				previousDots.SetInnerText("...");
				previousDots.AddCssClass("previous-dots");
				var routingValues = new RouteValueDictionary(routeValues){{"page", groupStart - cGroupSize}, {"pageSize", pageSize}};
				previousDots.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
				container.InnerHtml += previousDots.ToString();
			}

			for (var i = groupStart; i <= groupEnd; i++)
			{
				var pageNumber = new TagBuilder("a");
				pageNumber.AddCssClass(((i == currentPage)) ? "selected-page" : "page");
				pageNumber.SetInnerText((i).ToString());
				var routingValues = new RouteValueDictionary(routeValues) {{"page", i}, {"pageSize", pageSize}};
				pageNumber.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
				container.InnerHtml += pageNumber.ToString();
			}

			// if there are still pages past the end of this page group    
			if (pageCount > groupEnd)
			{
				var nextDots = new TagBuilder("a");
				nextDots.SetInnerText("...");
				nextDots.AddCssClass("next-dots");
				var routingValues = new RouteValueDictionary(routeValues) {{"page", groupEnd + 1}, {"pageSize", pageSize}};
				nextDots.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
				container.InnerHtml += nextDots.ToString();
			}

			// if we still have pages left to show     
			if (currentPage < pageCount)
			{
				var next = new TagBuilder("a");
				next.SetInnerText(">");
				next.AddCssClass("next");
				var routingValues = new RouteValueDictionary(routeValues) {{"page", currentPage + 1}, {"pageSize", pageSize}};
				next.MergeAttribute("href", urlHelper.Action(actionName, routingValues));
				container.InnerHtml += next.ToString();
			}

			return MvcHtmlString.Create(container.ToString());
		}

		#endregion

		public static string Lookup<T, TProperty>(this HtmlHelper<T> html, Expression<Func<T, TProperty>> expression)
		{
			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null) return null;
			return memberExpression.Member.Name;
		}

		public static MvcHtmlString DisplayWithBreaksFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string separator = "")
		{
			var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			var seperateWith = string.IsNullOrWhiteSpace(separator) ? System.Environment.NewLine : separator;
			var model = htmlHelper.Encode(metaData.Model).Replace(seperateWith, "<br />" + seperateWith);
			if (string.IsNullOrWhiteSpace(model)) return MvcHtmlString.Empty;
			return MvcHtmlString.Create(model);
		}

	}
}