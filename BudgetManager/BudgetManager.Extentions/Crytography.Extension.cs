using BudgetManager.Common;

namespace BudgetManager.Extentions
{
	public static class CrytographyExtensions
	{
		/// <summary>
		/// Hashes the string.
		/// </summary>
		/// <param name="inputString">The input string.</param>
		/// <returns></returns>
		public static string ToPasswordHash(this string inputString)
		{
			if (string.IsNullOrEmpty(inputString))
			{
				return null;
			}
			const string type = "SHA256";
			var text = "_SALT_" + inputString + "__B^dg#tM@n@g#r__";
			var hash = Crytography.ToHash(type, text);
			return hash.ToString();
		}
	}
}