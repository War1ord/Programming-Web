using System;
using System.ComponentModel;

namespace RequestForService.Business.Extensions
{
	public static class EnumExtensions
	{
		public static string ToDescription<T>(this T value)
		{
			if (value == null) throw new ArgumentNullException("value");
			var description = value.ToString();
			var fieldInfo = value.GetType().GetField(description);
			var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Length > 0)
			{
				description = attributes[0].Description;
			}
			return description;
		}
	}
}