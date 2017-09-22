using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace BudgetManager.Extentions
{
	/// <summary>
	/// Contains all the Enum Extensions
	/// </summary>
	public static class EnumExtension
	{
		/// <summary>
		/// Gets the enum description.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string GetEnumDescription<TEnum>(TEnum value)
		{
			FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes =
				(DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof (DescriptionAttribute), false);
			if ((attributes != null) && (attributes.Length > 0))
			{
				return attributes[0].Description;
			}
			else
			{
				return value.ToString();
			}
		}

		/// <summary>
		/// To the select list.
		/// </summary>
		/// <param name="enumValue">The enum value.</param>
		/// <returns></returns>
		public static IEnumerable<SelectListItem> ToSelectList(this Enum enumValue)
		{
			return from Enum e in Enum.GetValues(enumValue.GetType())
			       select new SelectListItem
			              {
				              Selected = e.Equals(enumValue),
				              Text = e.ToDescription(),
				              Value = e.ToString()
			              };
		}

		/// <summary>
		/// To the description.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		public static string ToDescription(this Enum value)
		{
			var attributes = (DescriptionAttribute[]) value.GetType().GetField(
				value.ToString()).GetCustomAttributes(typeof (DescriptionAttribute), false);
			return attributes.Length > 0 ? attributes[0].Description : value.ToString();
		}
	}
}