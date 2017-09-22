using System.Collections.Generic;
using System.Text;

namespace BudgetManager.Extentions
{
	public static class StringExtention
	{
		public static string Concatenate(this IEnumerable<string> source)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in source)
			{
				stringBuilder.Append(value + System.Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		public static string Concatenate(this IEnumerable<string> source, string seperator)
		{
			return string.Join(seperator, source);
		}

	    public static string ToLowerSolidString(this string source)
	    {
	        return source.ToLower().Replace(" ", "_");
	    }
	}
}