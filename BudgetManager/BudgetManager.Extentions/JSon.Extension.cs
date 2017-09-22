using System;
using System.Web.Script.Serialization;

namespace BudgetManager.Extentions
{
	public static class JSonExtension
	{
		public static string ToJson(this Object obj)
		{
			return new JavaScriptSerializer().Serialize(obj);
		}
	}
}